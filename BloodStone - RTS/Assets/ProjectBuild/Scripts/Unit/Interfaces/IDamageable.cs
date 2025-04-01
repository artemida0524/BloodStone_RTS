using System;

namespace Unit
{
    public interface IDamageable
    {
        event Action<int> OnTakeDamage;
        void TakeDamage(int amount);
    }


}

