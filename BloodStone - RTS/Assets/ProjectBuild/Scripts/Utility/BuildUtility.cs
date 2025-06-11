using Game.Gameplay.Build;
using System;

public class BuildUtility
{

    public static event Action<BuildBase> OnBuildEnable;
    public static event Action<BuildBase> OnBuildDisableOrDestroy;

    public static event Action<BuildBase> OnBuildWasBuilt;
    public static event Action<BuildBase> OnBuildWasBroken;

    public static void OnBuildDisableOrDestroyInvoke(BuildBase build)
    {
        OnBuildDisableOrDestroy?.Invoke(build);
    }

    public static void OnBuildEnableInvoke(BuildBase build)
    {
        OnBuildEnable?.Invoke(build);
    }

    public static void OnBuildWasBuiltInvoke(BuildBase build)
    {
        OnBuildWasBuilt?.Invoke(build);
    }

    public static void OnBuildWasBrokenInvoke(BuildBase build)
    {
        OnBuildWasBroken?.Invoke(build);
    }

}