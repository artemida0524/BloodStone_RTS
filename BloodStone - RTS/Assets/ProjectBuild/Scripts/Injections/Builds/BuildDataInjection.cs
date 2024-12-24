using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Build;

public class BuildDataInjection : MonoInstaller
{

    public override void InstallBindings()
    {
        Container
            .Bind<GlobalBuildsDataHandler>()
            .AsSingle()
            .NonLazy()
            ;
            

    }
}
