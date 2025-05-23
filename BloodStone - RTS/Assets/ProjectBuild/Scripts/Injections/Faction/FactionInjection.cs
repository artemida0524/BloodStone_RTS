using BloodStone.Gameplay.Build;
using UnityEngine;
using Zenject;

public class FactionInjection : MonoInstaller
{
    [SerializeField] private Headquarters faction;
    public override void InstallBindings()
    {
        Container
            .BindInstance(faction)
            .AsSingle();
    }
}
