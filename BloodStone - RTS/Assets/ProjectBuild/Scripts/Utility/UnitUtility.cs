using System;
using UnityEngine;

namespace Game.Gameplay.Units.Utils
{
    public class UnitUtility
    {
        public static event Action<UnitBase> OnUnitEnable;
        public static event Action<UnitBase> OnUnitDisableOrDestroy;

        public static event Action<UnitBase> OnUnitAdd;
        public static event Action<UnitBase> OnUnitRemove;




        public static void OnUnitDisableOrDestroyInvoke(UnitBase unit)
        {
            OnUnitDisableOrDestroy?.Invoke(unit);
        }

        public static void OnUnitEnableInvoke(UnitBase unit)
        {
            Debug.Log("OnUnitEnable");
            OnUnitEnable?.Invoke(unit);
        }

        public static void OnUnitAddInvoke(UnitBase unit)
        {
            Debug.Log("OnUnitAdd");
            OnUnitAdd?.Invoke(unit);
        }

        public static void OnUnitRemoveInvoke(UnitBase unit)
        {
            Debug.Log("OnUnitRemove");
            OnUnitRemove?.Invoke(unit);
        }
    } 
}