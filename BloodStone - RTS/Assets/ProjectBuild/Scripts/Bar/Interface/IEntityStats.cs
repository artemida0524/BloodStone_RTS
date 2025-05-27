using Game.Gameplay.Stats;
using System.Collections.Generic;

namespace Bar
{
    public interface IEntityStats
    {
        IEnumerable<IStat> EntityStats { get; }
    }

}