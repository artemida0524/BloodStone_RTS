using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using Build;
using GlobalData;

namespace Faction
{
    [Serializable]
    public class FactionDataHandler
    {
        public InteractionMode InteractionMode { get; private set; }

        public event Action<InteractionMode> OnInteractionModeChanged;

        public void ChangeInteractionMode(InteractionMode interactionMode)
        {
            InteractionMode = interactionMode;
            OnInteractionModeChanged?.Invoke(interactionMode);
        }

        public List<T> GetUnits<T>()
        {
            List<T> units = GlobalUnitsDataHandler.GetUnits<T>();
            return units;
        }
        
        public List<T> GetBuilds<T>()
        {
            List<T> builds = GlobalBuildsDataHandler.GetBuilds<T>();
            return builds;
        }

        public List<T> GetAll<T>()
        {
            var units = GetUnits<T>().OfType<T>();
            var builds = GetBuilds<T>().OfType<T>();

            List<T> result = new List<T>();

            result.AddRange(units);
            result.AddRange(builds);

            return result;
        }


    }
}