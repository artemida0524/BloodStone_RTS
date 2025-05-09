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
using Interaction;

namespace Select
{
    public class SelectedController : MonoBehaviour
    {
        [SerializeField] private LayerMask ground;
        [SerializeField] private MeshRenderer pointPositionerMeshRenderer;

        private Build.Faction faction;
        private SelectableHandler selectableHandler;

        private IReadOnlyList<ISelectable> selectedEntities = new List<ISelectable>();
        new private Camera camera;

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

        private void OnSelectedUnit(IReadOnlyList<ISelectable> units)
        {
            this.selectedEntities = units;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    //if (hitInfo.collider.TryGetComponent(out EntityBase entity))
                    //{
                    //    if (entity.FactionType == faction.FactionType)
                    //    {
                    //        if (entity is BuildInteractableBase build)
                    //        {
                    //            if (faction.Data.InteractionMode == InteractionMode.Setable)
                    //            {
                    //                var units = selectedEntities.OfType<UnitBase>().ToList();

                    //                build.Interact(units);
                    //                selectableHandler.UnselectAll();
                    //            }
                    //            else if (faction.Data.InteractionMode == InteractionMode.None)
                    //            {
                    //                build.Interact();
                    //            }
                    //        }
                    //        return;
                    //    }
                    //    else if (entity.FactionType == FactionType.Systems)
                    //    {
                    //        if (entity is BuildInteractableBase build)
                    //        {
                    //            if (faction.Data.InteractionMode == InteractionMode.Setable)
                    //            {
                    //                var units = selectedEntities.OfType<UnitBase>().ToList();

                    //                build.Interact(units);
                    //                selectableHandler.UnselectAll();
                    //            }
                    //            else if (faction.Data.InteractionMode == InteractionMode.None)
                    //            {
                    //                build.Interact();
                    //            }
                    //        }
                    //        return;
                    //    }
                    //    else if (entity.FactionType != faction.FactionType)
                    //    {
                    //        List<AttackingUnitBase> attackingUnits = selectedEntities.OfType<AttackingUnitBase>().ToList();

                    //        foreach (var item in attackingUnits)
                    //        {
                    //            item.SetState(new AttackAndFollowState(item, entity));
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    Vector3 positionArea = hitInfo.point;
                    //    positionArea.y = 0f;

                    //    pointPositionerMeshRenderer.transform.position = positionArea;

                    //    float scaleFactor = (float)selectedEntities.Count / 10;

                    //    scaleFactor = Mathf.Clamp(scaleFactor, 0.3f, 1.5f);

                    //    Vector3 scale = new Vector3(scaleFactor, pointPositionerMeshRenderer.transform.localScale.y, scaleFactor);

                    //    pointPositionerMeshRenderer.transform.localScale = scale;

                    //    foreach (var item in selectedEntities)
                    //    {
                    //        try
                    //        {
                    //            if (item is UnitBase unit)
                    //            {
                    //                unit.SetState(new MoveState(unit, GetPosition(), 0));
                    //            }
                    //        }
                    //        catch (System.Exception ex)
                    //        {
                    //            Debug.LogWarning(ex);
                    //        }
                    //    }
                    //}

                    if (hitInfo.collider.TryGetComponent(out IEntity entity))
                    {
                        if (entity.FactionType == faction.FactionType || entity.FactionType == FactionType.Systems)
                        {
                            if (selectedEntities.Count > 0)
                            {
                                if (entity is IInteractableSelectables interaction)
                                {
                                    interaction.Interact(selectedEntities);
                                }
                            }
                            else
                            {
                                if(entity is IInteractable interaction)
                                {
                                    interaction.Interact();
                                }
                            }
                        }

                    }
                    else
                    {
                        Vector3 positionArea = hitInfo.point;
                        positionArea.y = 0f;

                        pointPositionerMeshRenderer.transform.position = positionArea;

                        float scaleFactor = (float)selectedEntities.Count / 10;

                        scaleFactor = Mathf.Clamp(scaleFactor, 0.3f, 1.5f);

                        Vector3 scale = new Vector3(scaleFactor, pointPositionerMeshRenderer.transform.localScale.y, scaleFactor);

                        pointPositionerMeshRenderer.transform.localScale = scale;

                        foreach (var item in selectedEntities)
                        {
                            try
                            {
                                if (item is UnitBase unit)
                                {
                                    unit.SetState(new MoveState(unit, GetPosition(), 0));
                                }
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