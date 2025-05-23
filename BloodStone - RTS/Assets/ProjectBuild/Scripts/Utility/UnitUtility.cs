using System;
using UnityEngine;

namespace BloodStone.Gameplay.Units.Utils
{
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
            Debug.Log("OnUnitEnable");
            OnUnitEnable?.Invoke(unit);
        }
    } 
}