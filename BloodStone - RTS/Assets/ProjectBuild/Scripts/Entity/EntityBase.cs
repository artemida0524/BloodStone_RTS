using Data;
using Faction;
using System;
using UnityEngine;


namespace Entity
{
    public abstract class EntityBase : MonoBehaviour
    {
        public abstract Renderer Renderer { get; protected set; } 
        [field: SerializeField] public FactionType FactionType { get; protected set; } = FactionType.Team0;
        protected int HP { get; set; }
        
        public event Action OnFactionChanged;

        public void ChangeFaction(FactionType faction)
        {
            FactionType = faction;
            OnFactionChanged?.Invoke();
        }

        public abstract void Initialization(FactionType type, EntityCollectionData collectionData);
    }

}