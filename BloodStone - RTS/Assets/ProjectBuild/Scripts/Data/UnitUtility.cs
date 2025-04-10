using System;
using Unit;

public class UnitUtility
{
    public static event Action<UnitBase> OnUnitEnable;
    public static event Action<UnitBase> OnUnitDisableOrDestroy;

    public static void OnUnitDisableOrDestroyInvoke(UnitBase unit)
    {
        OnUnitDisableOrDestroy?.Invoke(unit);
    }

    public static void OnUnitEnableInvoke(UnitBase unit)
    {
        OnUnitEnable?.Invoke(unit);
    }
}