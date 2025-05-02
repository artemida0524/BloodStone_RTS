using Build;
using Zenject;

public class BuildSystemInject : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<BuildSystem>()
            .AsSingle()
            .NonLazy();
    }
}
