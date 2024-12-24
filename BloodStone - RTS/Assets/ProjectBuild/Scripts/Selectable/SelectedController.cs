using System.Collections.Generic;
using UnityEngine;
using Unit;
using Faction;
using System.Linq;
using State;
using Entity;
using Build;

namespace Select
{
    public class SelectedController : MonoBehaviour
    {
        [SerializeField] private SelectableHandler selectableHandler;
        [SerializeField] private Build.Faction faction;

        [SerializeField] private LayerMask ground;

        private List<UnitBase> selectedUnits = new List<UnitBase>();
        private Camera camera;

        private void Awake()
        {
            selectableHandler.OnSelectedUnits += OnSelectedUnit;
            camera = Camera.main;
        }

        private void OnSelectedUnit(List<UnitBase> units)
        {
            this.selectedUnits = units;
        }


        private void Update()
        {
            Debug.Log(faction.Data.InteractionMode);
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
                                    build.Interaction(selectedUnits);
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
                        foreach (var item in selectedUnits)
                        {
                            try
                            {
                                item.MoveTo(hitInfo.point);
                            }
                            catch (System.Exception)
                            {
                            }
                        }
                    }
                }
            }
        }
    }
}