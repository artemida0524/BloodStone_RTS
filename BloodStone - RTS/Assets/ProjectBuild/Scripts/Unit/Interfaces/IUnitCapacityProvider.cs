using Game.Gameplay.Stats;
using System;

namespace Game.Gameplay.Units
{

    public interface IUnitCapacityProvider
    {
        IStat CurrentUnitsStat { get; }
        int HasSpace { get; }
        event Action<IStat> OnUnitsStatsChanged;

        void Init();
        bool CheckSpace(int amount);
    }

}