using Select;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Unit;
using UnityEditor.Rendering;
using Unity.VisualScripting;
using System.Net;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

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

        entitySelectedGrid.Init(GetInteractables(selectables));




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
