using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using Build;
using GlobalData;
using System.Collections;

namespace Faction
{
    [Serializable]
    public class FactionDataHandler
    {
        public InteractionMode InteractionMode { get; private set; }

        public event Action<InteractionMode> OnInteractionModeChanged;

        public IBuildingProvider BuildingProvider { get; set; }

        public void ChangeInteractionMode(InteractionMode interactionMode)
        {
            InteractionMode = interactionMode;
            OnInteractionModeChanged?.Invoke(interactionMode);
        }

        public IEnumerable<T> GetUnits<T>()
        {
            IEnumerable<T> units = GlobalUnitsDataHandler.GetUnits<T>();
            return units;
        }
        
        public IEnumerable<T> GetBuilds<T>()
        {
            IEnumerable<T> builds = BuildingProvider.GetBuilds<T>();
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