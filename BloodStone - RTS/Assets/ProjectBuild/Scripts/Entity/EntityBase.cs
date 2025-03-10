using Faction;
using System;
using System.Threading.Tasks;
using UnityEngine;


namespace Entity
{
    public abstract class EntityBase : MonoBehaviour
    {
        public abstract Renderer BodyRenderer { get; protected set; } 
        public abstract Vector3 Position { get; }
        public abstract float Radius { get; }
        [field: SerializeField] public FactionType FactionType { get; protected set; }
        
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
    }
}