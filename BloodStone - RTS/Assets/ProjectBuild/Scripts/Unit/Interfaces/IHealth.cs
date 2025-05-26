using System;

namespace Game.Gameplay
{
    public interface IHealth
    {
        int MaxCountHealth { get; }
        int CountHealth { get; }
        bool IsMaxHealth { get; }

        void AddHealth(int amount);
        void SpendHealth(int amount);

        event Action<int> OnHealthChange;
    }
}