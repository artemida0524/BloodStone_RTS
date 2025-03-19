using Select;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Unit;
using Option;

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
                entitySelectedGrid.Init(GetInteractables(selectables)); 
            }
        }


        private List<IInteractable> GetInteractables(List<ISelectable> selectables)
        {
            List<IInteractable> interactables = new List<IInteractable>();
            foreach (var item in selectables)
            {
                interactables.Add(item.SelectInfo);
            }

            return interactables;
        }
    }

}