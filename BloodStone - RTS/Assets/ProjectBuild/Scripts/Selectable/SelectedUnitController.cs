using System.Collections.Generic;
using UnityEngine;
using Unit;
using Faction;
using System.Linq;
using State;
namespace Select
{
    public class SelectedUnitController : MonoBehaviour
    {
        [SerializeField] private SelectableHandler selectableView;
        [SerializeField] private FactionData faction;

        [SerializeField] private LayerMask ground;

        private List<UnitBase> units;
        private Camera camera;

        private void Awake()
        {
            selectableView.OnSelectedUnits += OnSelectedUnit;
            camera = Camera.main;
        }

        private void OnSelectedUnit(List<UnitBase> units)
        {
            Debug.Log(units.Count);

            this.units = units;
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (faction.InteractionMode == InteractionMode.Setable)
                {
                    if (units != null && units.Count > 0)
                    {
                        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out RaycastHit hitInfo/*, 10000f, ground*/))
                        {

                            if (hitInfo.collider.TryGetComponent(out UnitBase unit))
                            {
                                if (unit.FactionType != faction.FactionType)
                                {

                                    List<AttackingUnitBase> attackingUnits = units.Where(unit => unit is AttackingUnitBase).Cast<AttackingUnitBase>().ToList();


                                    foreach (var item in attackingUnits)
                                    {
                                        item.StateInteractable.SetState(new AttackAndFollowState(item, unit));
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in units)
                                {
                                    item.MoveTo(hitInfo.point);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}