using Faction;
using UnityEngine;

namespace Game.Gameplay.Entity
{
    public interface IEntity
    {
        Vector3 Position { get; }
        float Radius { get; }

        FactionType FactionType { get; }
    }

}