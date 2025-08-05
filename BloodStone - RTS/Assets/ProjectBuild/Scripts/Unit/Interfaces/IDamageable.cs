using Game.Gameplay.Entity;
using System;

namespace Game.Gameplay
{
    public interface IDamageable : IEntity
    {
        event Action<int> OnTakeDamage;
        void TakeDamage(int amount);
    }
}