using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Build;
using GlobalData;

public class GlobalDataInjection : MonoInstaller
{

    public override void InstallBindings()
    {
        Container
            .Bind<GlobalBuildsGridDataHandler>()
            .AsSingle()
            .NonLazy()
            ;


        Container
            .Bind<GlobalUnitsDataHandler>()
            .AsSingle()
            .NonLazy()
            ;

    }
}
