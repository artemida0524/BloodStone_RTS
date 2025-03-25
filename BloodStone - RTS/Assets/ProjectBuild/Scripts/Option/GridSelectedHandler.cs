using Select;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Unit;
using Weapon;
using UnityEngine.UI;

namespace Option
{
    public class GridSelectedHandler : MonoBehaviour
    {
        [SerializeField] private EntitySelectedGrid entitySelectedGrid;
        [SerializeField] private OptionSelectedGrid optionSelectedGrid;



        [Inject]
        private void Construct(SelectableHandler handler)
        {
            handler.OnSelectedUnits += OnSelectedUnitsHandler;
        }

        private void OnSelectedUnitsHandler(IReadOnlyList<ISelectable> selectables)
        {
            entitySelectedGrid.RemoveAll();
            optionSelectedGrid.RemoveAll();

                
            if (selectables.Count > 0)
            {
                entitySelectedGrid.Init(selectables);
            }
        }
    }

}