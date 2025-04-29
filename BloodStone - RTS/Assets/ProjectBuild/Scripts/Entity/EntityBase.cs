using Faction;
using System;
using UnityEngine;

namespace Entity
{
    [DisallowMultipleComponent]
    public abstract class EntityBase : MonoBehaviour, IEntity
    {
        public abstract Renderer BodyRenderer { get; protected set; } 
        public abstract Vector3 Position { get; }
        public abstract float Radius { get; }
        [field: SerializeField] public FactionType FactionType { get; protected set; }
        [field: SerializeField] public EntityInfoSO EntityInfo { get; protected set; }
        
        public event Action OnFactionChanged;

        public void ChangeFaction(FactionType faction)
        {
            FactionType = faction;
            OnFactionChanged?.Invoke();
        }

        public virtual void InitializationEntity(FactionType type)
        {
            this.FactionType = type;
        }

        public void Delete()
        {
            transform.position += new Vector3(10, 0, 0);
        }
    }
}