using Select;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Unit;

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

        private void OnSelectedUnitsHandler(List<ISelectable> selectables)
        {
            entitySelectedGrid.RemoveAll();
            optionSelectedGrid.RemoveAll();

            if (selectables.Count > 0)
            {
                entitySelectedGrid.Init(GetOptions(selectables));
            }
        }


        private List<IOption> GetOptions(List<ISelectable> selectables)
        {
            List<IOption> options = new List<IOption>();
            foreach (var item in selectables)
            {
                options.Add(item.Options);
            }

            return options;
        }
    }

}