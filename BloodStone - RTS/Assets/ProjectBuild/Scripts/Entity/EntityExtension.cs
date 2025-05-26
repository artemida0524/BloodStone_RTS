using Game.Gameplay.Entity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Entity
{
    public static class EntityExtension
    {
        public static TSource NearestEntity<TSource>(this IEnumerable<TSource> entities, IEntity entity) where TSource : class, IEntity
        {

            if(entities.Count() == 0)
            {
                Debug.LogWarning("Zero Entities");
                return null;
            }

            float distance = float.PositiveInfinity;
            TSource nearestEntity = entities.GetEnumerator().Current;

            foreach (var item in entities)
            {
                float newDistance = Vector3.Distance(entity.Position, item.Position) + item.Radius;
                if (newDistance < distance)
                {
                    distance = newDistance;
                    nearestEntity = item;
                }
            }
            return nearestEntity;
        }
    }

}