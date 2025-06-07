using System;
using System.Collections.Generic;
using System.Linq;
using GlobalData;
using Game.Gameplay.Units.Providers;
using Game.Gameplay.Entity;

namespace Faction
{
    public class FactionDataHandler
    {
        private IUnitProvider _unitsProvider;
        private IBuildingProvider _buildingProvider;
        private FactionType _factionType;

        public FactionDataHandler(IBuildingProvider buildingProvider, IUnitProvider unitsProvider, FactionType factionType)
        {
            _buildingProvider = buildingProvider;
            _unitsProvider = unitsProvider;
            _factionType = factionType;
        }

        public IEnumerable<T> GetUnits<T>() where T : IEntity
        {
            IEnumerable<T> units = _unitsProvider.GetUnits<T>().Where(unit => unit.FactionType == _factionType);
            return units;   
        }

        public IEnumerable<T> GetUnits<T>(Func<T, bool> predicat) where T : IEntity
        {
            IEnumerable<T> units = _unitsProvider.GetUnits<T>(predicat).Where(unit => unit.FactionType == _factionType);
            return units;
        }

        public IEnumerable<T> GetBuilds<T>() where T : IEntity
        {
            IEnumerable<T> builds = _buildingProvider.GetBuilds<T>().Where(build => build.FactionType == _factionType);
            return builds;
        }

        public IEnumerable<T> GetBuilds<T>(Func<T, bool> predicat) where T : IEntity
        {
            IEnumerable<T> units = _buildingProvider.GetBuilds<T>(predicat).Where(build => build.FactionType == _factionType);
            return units;
        }

        public List<T> GetAll<T>() where T : IEntity
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