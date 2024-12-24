using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using Build;

namespace Faction
{
    [Serializable]
    public class FactionDataHandler
    {
        [field: SerializeField] public List<UnitBase> UnitsCollect { get; private set; }
        [field: SerializeField] public List<BuildBase> BuildsCollect { get; private set; }

        public FactionType FactionType { get; private set; }
        public InteractionMode InteractionMode { get; private set; }

        public void Initialize(FactionType FactionType)
        {
            this.FactionType = FactionType;

            foreach (var item in UnitsCollect)
            {
                item.InitializationEntity(FactionType);
            }

            foreach (var item in BuildsCollect)
            { 
                item.InitializationEntity(FactionType);
            }
        }

        public void ChangeInteractionMode(InteractionMode interactionMode)
        {
            InteractionMode = interactionMode;
        }

        public void SetUnit<T>(T unit) where T : UnitBase
        {
            UnitsCollect.Add(unit);
        }

        public void SetBuild<T>(T build) where T : BuildBase
        {
            BuildsCollect.Add(build);
        }

        public List<T> GetUnits<T>() where T : UnitBase
        {
            List<T> units = UnitsCollect.Where(t => t is T).Cast<T>().ToList();
            return units;
        }

        public List<T> GetBuilds<T>() where T : BuildBase
        {
            List<T> builds = BuildsCollect.Where(t => t is T).Cast<T>().ToList();
            return builds;
        }





    }
}