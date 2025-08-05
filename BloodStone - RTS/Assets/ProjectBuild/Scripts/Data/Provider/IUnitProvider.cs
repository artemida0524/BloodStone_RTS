using System.Collections.Generic;
using Unit;
using UnityEngine;
using System;

namespace Game.Gameplay.Units.Providers
{
    public interface IUnitProvider
    {
        IEnumerable<UnitBase> AllUnits { get; }

        void Init();

        void AddUnit(UnitBase unit);
        IEnumerable<T> GetUnits<T>();
        IEnumerable<T> GetUnits<T>(Func<T, bool> predicate);

        void PlaceUnit(UnitBase unit, Vector3 position);

        void RemoveUnit(UnitBase unit);
    }
}