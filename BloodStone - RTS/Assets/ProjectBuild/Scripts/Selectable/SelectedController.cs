using System.Collections.Generic;
using UnityEngine;
using Faction;
using State;
using Zenject;
using UnityEngine.AI;
using Interaction;
using Game.Gameplay.Build;
using Game.Gameplay.Units;
using Game.Gameplay.Selection;
using Game.Gameplay.Entity;

namespace Select
{
    public class SelectedController : MonoBehaviour
    {
        [SerializeField] private LayerMask ground;
        [SerializeField] private MeshRenderer pointPositionerMeshRenderer;

        private Headquarters faction;
        private SelectableHandler selectableHandler;

        private IReadOnlyList<ISelectable> selectedEntities = new List<ISelectable>();
        new private Camera camera;

        [Inject]
        private void Construct(SelectableHandler selectableHandler, Headquarters faction)
        {
            this.selectableHandler = selectableHandler;
            this.faction = faction;
        }

        public void Init()
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