using Zenject;
using UnityEngine;

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
