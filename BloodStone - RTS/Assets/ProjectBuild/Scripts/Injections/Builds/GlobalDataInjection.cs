using Zenject;
using GlobalData;
using Build;

public class GlobalDataInjection : MonoInstaller
{
    public BuildSystem system;

    public override void InstallBindings()
    {
        Container
            .BindInstance(system)
            .AsSingle()
            .NonLazy()
            ;

        Container
            .Bind<GlobalUnitsDataHandler>()
            .AsSingle()
            .NonLazy()
            ;

        Container
            .Bind<GlobalBuildsDataHandler>()
            .AsSingle()
            .NonLazy()
            ;
    }
}
