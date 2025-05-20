using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace GlobalData
{
    public interface IUnitProvider
    {
        IEnumerable<UnitBase> AllUnits { get; }

        void Init();

        void AddUnit(UnitBase unit);
        IEnumerable<T> GetUnits<T>();

        void PlaceUnit(UnitBase unit, Vector3 position);

        void RemoveUnit(UnitBase unit);
    }
}