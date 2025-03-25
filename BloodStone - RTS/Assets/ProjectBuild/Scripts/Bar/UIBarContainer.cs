using Bar;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBarContainer : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private UIBar uiBarPrefab;

    [SerializeField] private UIBarDataSOList uiBarDataSOList;

    private List<UIBar> bars;

    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }


    public void AddBar(IBar bar)
    {
        if (bars == null)
        {
            bars = new();   
        }


        foreach (var item in uiBarDataSOList.BarDataList)
        {

            if (bar.Name == item.Name)
            {
                UIBar barInstance = Instantiate(uiBarPrefab, container);
                barInstance.Init(bar, item);

                bars.Add(barInstance);

                return;
            }

        }


        Debug.LogError("NotFound");

    }

    public void RemoveBar(string name)
    {
        foreach (var item in bars)
        {
            if (item.ResourceBar.Name == name)
            {
                item.Dispose();
                bars.Remove(item);

                Destroy(item.gameObject);

                return;
            }
        }

        throw new Exception("NotFound");
    }

}
