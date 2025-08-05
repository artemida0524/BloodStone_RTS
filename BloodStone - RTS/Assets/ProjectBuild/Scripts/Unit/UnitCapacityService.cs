using Game.Gameplay.Build;
using Game.Gameplay.Stats;
using Game.Gameplay.Units.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
namespace Game.Gameplay.Units
{

    public class UnitCapacityService : IUnitCapacityProvider
    {
        private Headquarters _headquarters;
        private MaxCountUnitsStat _maxCountUnits = new MaxCountUnitsStat();

        private List<UnitBase> _units;
        private List<IHut> _huts;

        public int HasSpace => _countMaxCountUnit - _unitsCost;
        private int _unitsCost => _units.Sum(unit => unit.HousingCost);
        private int _countMaxCountUnit => Huts.Sum(hut => hut.MaxUnitCount);


        public IStat CurrentUnitsStat => _maxCountUnits;

        public List<IHut> Huts { get => _huts; set => _huts = value; }

        public event Action<IStat> OnUnitsStatsChanged;

        [Inject]
        private void Construct(Headquarters headquarters)
        {
            _headquarters = headquarters;
        }

        public void Init()
        {
            _units = new List<UnitBase>(_headquarters.Data.GetUnits<UnitBase>());
            Huts = new List<IHut>(_headquarters.Data.GetBuilds<IHut>());

            UpdateCount();

            UnitUtility.OnUnitAdd += OnUnitAddHandler;
            UnitUtility.OnUnitRemove += OnUnitRemoveHandler;

            BuildUtility.OnBuildWasBuilt += OnBuildWasBuiltHandler;
            BuildUtility.OnBuildWasBroken += OnBuildWasRemoveHandler;

        }

        private void OnUnitAddHandler(UnitBase unit)
        {
            if (unit.FactionType == _headquarters.FactionType)
            {
                if (!_units.Contains(unit))
                {
                    _units.Add(unit);
                    UpdateCount();
                }
            }
        }

        private void OnUnitRemoveHandler(UnitBase unit)
        {
            if (unit.FactionType == _headquarters.FactionType)
            {

                if (_units.Contains(unit))
                {
                    _units.Remove(unit);
                    UpdateCount();
                }
            }
        }

        private void OnBuildWasBuiltHandler(BuildBase build)
        {
            if (build.FactionType == _headquarters.FactionType && build is IHut hut)
            {

                if (!Huts.Contains(hut))
                {
                    Huts.Add(hut);
                    UpdateCount();
                }

            }

        }

        private void OnBuildWasRemoveHandler(BuildBase build)
        {

            if (build.FactionType == _headquarters.FactionType && build is IHut hut)
            {

                if (Huts.Contains(hut))
                {
                    Huts.Remove(hut);
                    UpdateCount();
                }

            }
        }


        private void UpdateCount()
        {
            _maxCountUnits.UpdateData(_unitsCost, _countMaxCountUnit);
            OnUnitsStatsChanged?.Invoke(_maxCountUnits);


            Debug.Log("MaxCountUnit: " + _maxCountUnits.MaxCount + " Count: " + _maxCountUnits.Count);
        }

        public bool CheckSpace(int amount)
        {
            return HasSpace >= amount;
        }

        private class MaxCountUnitsStat : IStat
        {
            public string Name => "MaxCountUnit";
            public int MaxCount { get; private set; }
            public int Count { get; private set; }


            public event EventHandler OnDataChange;

            public void UpdateData(int count, int maxCount)
            {
                Update(count, maxCount);
            }

            private void Update(int count, int maxCount)
            {
                Count = count;
                MaxCount = maxCount;
                OnDataChange?.Invoke(this, EventArgs.Empty);
            }

        }
    }
}