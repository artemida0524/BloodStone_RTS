using Bar;
using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBarContainer : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private UIBar uiBarPrefab;

    [SerializeField] private UIBarDataSOList uiBarDataSOList;

    private List<UIBar> bars;

    private CinemachineVirtualCamera cCamera;
    private float referenceFOV = 60f;
    private Vector2 baseScale = new Vector2(0.015f, 0.015f);


    private void Start()
    {
        cCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
    }


    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);

        float currentFOV = cCamera.m_Lens.FieldOfView;
        float scaleMultiplier = Mathf.Tan(Mathf.Deg2Rad * currentFOV * 0.5f) / Mathf.Tan(Mathf.Deg2Rad * referenceFOV * 0.5f);

        transform.localScale = new Vector3(baseScale.x, baseScale.y, 1f) * scaleMultiplier;



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
