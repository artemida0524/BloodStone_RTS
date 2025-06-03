using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using Build;
using GlobalData;
using System.Collections;
using Game.Gameplay.Units.Providers;

namespace Faction
{
    public class FactionDataHandler
    {
        private IUnitProvider _unitsProvider;
        private IBuildingProvider _buildingProvider;

        public FactionDataHandler(IBuildingProvider buildingProvider, IUnitProvider unitsProvider)
        {

            _buildingProvider = buildingProvider;
            _unitsProvider = unitsProvider;
        }


        public IEnumerable<T> GetUnits<T>()
        {
            IEnumerable<T> units = _unitsProvider.GetUnits<T>();
            return units;
        }
        
        public IEnumerable<T> GetBuilds<T>()
        {
            IEnumerable<T> builds = _buildingProvider.GetBuilds<T>();
            return builds;

        }

        public List<T> GetAll<T>()
        {
            IEnumerable<T> units = GetUnits<T>().OfType<T>();
            IEnumerable<T> builds = GetBuilds<T>().OfType<T>();

            List<T> result = new List<T>();

            result.AddRange(units);
            result.AddRange(builds);

            return result;
        }
    }
}