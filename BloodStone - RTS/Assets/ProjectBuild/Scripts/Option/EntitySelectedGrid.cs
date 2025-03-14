using System.Collections.Generic;
using Unit;
using UnityEngine;

public class EntitySelectedGrid : MonoBehaviour
{
    public List<EntitySelect> items;


    public void Init(List<IInteractable> interactables)
    {
        for (int i = 0; i < interactables.Count; i++)
        {
            items[i].SetInteractable(interactables[i]);
        }
    }


    public void RemoveAll()
    {
        foreach (var item in items)
        {
            item.Remove();
        }
    }
}