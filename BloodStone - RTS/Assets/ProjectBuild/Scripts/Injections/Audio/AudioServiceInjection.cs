using Game.Gameplay.Audio;
using UnityEngine;
using Zenject;

public class AudioServiceInjection : MonoInstaller
{
    [SerializeField] private AudioService audioService;

    public override void InstallBindings()
    {

        Container
            .BindInterfacesAndSelfTo<AudioService>()
            .FromInstance(audioService)
            .AsSingle()
            ;

    }
}
