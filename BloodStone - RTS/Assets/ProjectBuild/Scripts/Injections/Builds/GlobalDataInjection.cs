using Zenject;
using GlobalData;
using UnityEngine;
using Scripts.ObjectPool.Provider;
using Game.Gameplay.Units;

public class GlobalDataInjection : MonoInstaller
{
    [SerializeField] private PoolProviderTest _poolProviderTest;
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<GlobalUnitsDataHandler>()
            .AsSingle()
            .NonLazy()
            ;

        Container
            .BindInterfacesAndSelfTo<GlobalBuildsDataHandler>()
            .AsSingle()
            .NonLazy()
            ;



        Container
            .BindInterfacesAndSelfTo<UnitCapacityService>()
            .AsSingle();

    }
}
