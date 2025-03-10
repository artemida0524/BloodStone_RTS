using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

namespace GlobalData
{
    public class GlobalUnitsDataHandler
    {
        private static List<UnitBase> AllUnits { get; } = new List<UnitBase>();


        public static List<T> GetUnits<T>()
        {
            return AllUnits.Where(unit => unit is T).Cast<T>().ToList();
        }

        public static void AddUnit(UnitBase unit)
        {

            if (AllUnits.Contains(unit))
            {
                Debug.LogWarning("Already exists");
                return;
            }
            AllUnits.Add(unit);
        }

        public static void RemoveUnit(UnitBase unit)
        {
            AllUnits.Remove(unit);
        }


    } 
}