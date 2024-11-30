using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;

namespace Select
{
    public class SelectedUnitMovementController : MonoBehaviour
    {
        [SerializeField] private SelectableHandler selectableView;
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
                if (units != null && units.Count > 0)
                {
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hit, 10000f, ground))
                    {
                        foreach (var item in units)
                        {
                            item.MoveTo(hit.point);
                        }
                    }

                }
            }
        }

    }

}