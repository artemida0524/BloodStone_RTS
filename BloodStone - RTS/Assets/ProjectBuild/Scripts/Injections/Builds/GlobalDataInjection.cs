using Zenject;
using GlobalData;
using Build;

public class GlobalDataInjection : MonoInstaller
{

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
