using Faction;
using UnityEngine;

namespace Entity
{
    public interface IEntity
    {
        Vector3 Position { get; }
        float Radius { get; }

        FactionType FactionType { get; }
    }

}