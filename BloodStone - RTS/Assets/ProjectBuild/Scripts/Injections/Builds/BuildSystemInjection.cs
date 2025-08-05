using Game.Gameplay.Construction;
using Zenject;
using UnityEngine;

public class BuildSystemInjection : MonoInstaller
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
