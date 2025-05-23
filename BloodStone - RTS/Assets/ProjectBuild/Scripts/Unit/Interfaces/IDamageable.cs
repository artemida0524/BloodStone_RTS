using BloodStone.Gameplay.Entity;
using System;

namespace BloodStone.Gameplay
{
    public interface IDamageable : IEntity
    {
        event Action<int> OnTakeDamage;
        void TakeDamage(int amount);
    }
}