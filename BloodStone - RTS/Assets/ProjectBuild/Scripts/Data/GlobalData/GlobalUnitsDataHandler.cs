using Game.Gameplay.Units;
using Game.Gameplay.Units.Providers;
using Game.Gameplay.Units.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlobalData
{

    public class GlobalUnitsDataHandler : IUnitProvider
    {
        private List<UnitBase> _allUnits = new List<UnitBase>();

        public IEnumerable<UnitBase> AllUnits => _allUnits;


        public void Init()
        {
            Debug.Log("Init");
            UnitUtility.OnUnitEnable += OnUnitEnableHandler;
            UnitUtility.OnUnitDisableOrDestroy += OnUnitDisableOrDestroyHadler;

            UnitUtility.OnUnitAdd += AddUnitHandler;
            UnitUtility.OnUnitRemove += RemoveUnitHandler;
        }

        private void RemoveUnitHandler(UnitBase unit)
        {
            RemoveUnit(unit);
        }

        private void AddUnitHandler(UnitBase unit)
        {
            
            AddUnit(unit);
        }

        private void OnUnitEnableHandler(UnitBase unit)
        {
            AddUnit(unit);
        }

        private void OnUnitDisableOrDestroyHadler(UnitBase unit)
        {
            RemoveUnit(unit);
        }

        public IEnumerable<T> GetUnits<T>()
        {
            return _allUnits.OfType<T>();
        }

        public void AddUnit(UnitBase unit)
        {

            if (_allUnits.Contains(unit))
            {
                Debug.LogWarning("Already exists");
                return;
            }
            _allUnits.Add(unit);
        }

        public void PlaceUnit(UnitBase unit, Vector3 position)
        {
            unit.transform.position = position;
            AddUnit(unit);
            unit.Init();
        }

        public void RemoveUnit(UnitBase unit)
        {
            _allUnits.Remove(unit);
        }
    } 
}