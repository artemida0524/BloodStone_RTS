using Build;
using Zenject;
using UnityEngine;

public class BuildSystemInject : MonoInstaller
{
    [SerializeField] private BuildingSystem _buildSystem;
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<BuildingSystem>()
            .FromInstance(_buildSystem)
            .AsSingle();
    }
}
