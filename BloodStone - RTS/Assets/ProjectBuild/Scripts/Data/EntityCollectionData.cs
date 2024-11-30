using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unit;

namespace Data
{
    public class EntityCollectionData : MonoBehaviour
    {
        public List<UnitBase> UnitsCollect = new List<UnitBase>();
        //public List<BuildBase> BuildCollection = new List<BuildBase>();

        public List<T> GetUnits<T>() where T : UnitBase
        {
            List<T> units = UnitsCollect.Where(t => t is T).Cast<T>().ToList();

            return units;
        }

        public void SetUnit<T>(T unit) where T : UnitBase
        {
            UnitsCollect.Add(unit);
        }

    }

}