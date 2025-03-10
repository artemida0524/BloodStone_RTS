using Cinemachine;
using Select;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SelectedInjection : MonoInstaller
{
    [SerializeField] private SelectableHandler selectableHandler;
    [SerializeField] private SelectedController controller;

    public override void InstallBindings()
    {
        Container
            .BindInstance(selectableHandler)
            .AsSingle();

        Container
            .BindInstance(controller)
            .AsSingle();
    }
}
