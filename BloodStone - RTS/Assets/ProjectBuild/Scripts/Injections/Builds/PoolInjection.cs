using Zenject;
using UnityEngine;
using Scripts.ObjectPool.Provider;

public class PoolInjection : MonoInstaller
{
    [SerializeField] private PoolProviderTest _poolProviderTest;



    public override void InstallBindings()
    {

        Container
        .BindInstance(_poolProviderTest)
        .AsSingle();





    }
}
