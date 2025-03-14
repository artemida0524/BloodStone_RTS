using System.Collections.Generic;
using Unit;
using UnityEngine;
using static UnityEditor.Progress;

public class OptionSelectedGrid : MonoBehaviour
{
    public List<OptionSelected> items;

    public void Init(List<DoActionOption> interactables)
    {
        for (int i = 0; i < interactables.Count; i++)
        {
            items[i].SetAction(interactables[i]);
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




