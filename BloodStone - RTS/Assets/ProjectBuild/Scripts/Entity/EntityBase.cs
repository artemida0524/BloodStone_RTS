using Faction;
using System;
using UnityEngine;

namespace Game.Gameplay.Entity
{
    [DisallowMultipleComponent]
    public abstract class EntityBase : MonoBehaviour, IEntity
    {
        public abstract Renderer BodyRenderer { get; protected set; } 
        public abstract Vector3 Position { get; }
        public abstract float Radius { get; }
        [field: SerializeField] public FactionType FactionType { get; protected set; }
        [field: SerializeField] public EntityInfoSO EntityInfo { get; protected set; }
        
        public virtual bool CanInteraction { get; set; } = true;    

        public event Action<FactionType> OnFactionTypeChanged;

        public void ChangeFactiontype(FactionType faction)
        {
            FactionType = faction;
            OnFactionTypeChanged?.Invoke(FactionType);
        }

        public virtual void SetFactionType(FactionType type)
        {
            this.FactionType = type;
        }

        public void Delete()
        {
            transform.position += new Vector3(10, 0, 0);
        }
    }
}