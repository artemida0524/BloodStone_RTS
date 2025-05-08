using Zenject;
using GlobalData;
using Build;
using UnityEngine;
public class GlobalDataInjection : MonoInstaller
{
    [SerializeField] private PoolProviderTest _poolProviderTest;
    public override void InstallBindings()
    {
        Container
            .Bind<GlobalUnitsDataHandler>()
            .AsSingle()
            .NonLazy()
            ;

        Container
            .BindInterfacesAndSelfTo<GlobalBuildsDataHandler>()
            .AsSingle()
            .NonLazy()
            ;


    }
}
