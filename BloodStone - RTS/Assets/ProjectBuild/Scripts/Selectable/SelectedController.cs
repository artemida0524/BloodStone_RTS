using System.Collections.Generic;
using UnityEngine;
using Unit;
using Faction;
using System.Linq;
using State;
using Entity;
using Build;
using Zenject;
using UnityEngine.AI;

namespace Select
{
    public class SelectedController : MonoBehaviour 
    {
        [SerializeField] private LayerMask ground;
        [SerializeField] private MeshRenderer pointPositionerMeshRenderer;

        private Build.Faction faction;
        private SelectableHandler selectableHandler;

        private List<ISelectable> selectedUnits = new List<ISelectable>();
        private Camera camera;

        [Inject]
        private void Construct(SelectableHandler selectableHandler, Build.Faction faction)
        {
            this.selectableHandler = selectableHandler;
            this.faction = faction;
        }

        private void Awake()
        {
            selectableHandler.OnSelectedUnits += OnSelectedUnit;
            camera = Camera.main;
        }

        private void OnSelectedUnit(List<ISelectable> units)
        {
            this.selectedUnits = units;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent(out EntityBase entity))
                    {
                        if (entity.FactionType == faction.FactionType)
                        {
                            if (entity is BuildInteractableBase build)
                            {
                                if (faction.Data.InteractionMode == InteractionMode.Setable)
                                {
                                    var units = selectedUnits.Where(unit => unit is UnitBase).Cast<UnitBase>().ToList();

                                    build.Interaction(units);
                                    selectableHandler.UnselectAll();
                                }
                                else if (faction.Data.InteractionMode == InteractionMode.None)
                                {
                                    build.Interaction();
                                }
                            }
                            return;
                        }
                        else if (entity.FactionType != faction.FactionType)
                        {
                            List<AttackingUnitBase> attackingUnits = selectedUnits.Where(unit => unit is AttackingUnitBase).Cast<AttackingUnitBase>().ToList();

                            foreach (var item in attackingUnits)
                            {
                                item.StateInteractable.SetState(new AttackAndFollowState(item, entity));
                            }
                        }
                    }
                    else
                    {

                        Vector3 positionArea = hitInfo.point;
                        positionArea.y = 0f;

                        pointPositionerMeshRenderer.transform.position = positionArea;


                        float scaleFactor = (float)selectedUnits.Count / 10;

                        scaleFactor = Mathf.Clamp(scaleFactor, 0.3f, 1.5f);

                        Vector3 scale = new Vector3(scaleFactor, pointPositionerMeshRenderer.transform.localScale.y, scaleFactor);

                        pointPositionerMeshRenderer.transform.localScale = scale;

                        foreach (var item in selectedUnits)
                        {
                            try
                            {
                                ((IMovable)item).MoveTo(GetPosition());
                            }
                            catch (System.Exception ex)
                            {
                                Debug.LogWarning(ex);
                            }
                        }
                    }
                }
            }
        }

        private Vector3 GetPosition()
        {
            Vector3 newPos = new Vector3(Random.Range(pointPositionerMeshRenderer.bounds.min.x, pointPositionerMeshRenderer.bounds.max.x), Random.Range(pointPositionerMeshRenderer.bounds.min.y, pointPositionerMeshRenderer.bounds.max.y), Random.Range(pointPositionerMeshRenderer.bounds.min.z, pointPositionerMeshRenderer.bounds.max.z));



            return NavMesh.SamplePosition(newPos, out NavMeshHit navmeshhit, 0.1f, NavMesh.AllAreas) ? newPos : GetPosition();

        }
    }


}