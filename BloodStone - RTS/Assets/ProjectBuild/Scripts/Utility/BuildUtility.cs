using BloodStone.Gameplay.Build;
using System;

public class BuildUtility
{

    public static event Action<BuildBase> OnBuildEnable;
    public static event Action<BuildBase> OnBuildDisableOrDestroy;

    public static void OnBuildDisableOrDestroyInvoke(BuildBase build)
    {
        OnBuildDisableOrDestroy?.Invoke(build);
    }

    public static void OnBuildEnableInvoke(BuildBase build)
    {
        OnBuildEnable?.Invoke(build);
    }
}