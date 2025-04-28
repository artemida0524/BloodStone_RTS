using Entity;
using System;

namespace Unit
{
    public interface IDamageable : IEntity
    {
        event Action<int> OnTakeDamage;
        void TakeDamage(int amount);
    }
}