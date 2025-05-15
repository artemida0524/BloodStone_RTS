using System;
using UnityEngine;

namespace Currency
{
    public abstract class CurrencyBase : ICurrency
    {
        public virtual int Count { get; protected set; } = 1000;

        public virtual int MaxCount { get; protected set; } = 10000;

        public bool IsFull => Count >= MaxCount;

        public event Action<int> OnValueChange;

        public void Add(int amount)
        {
            Count = Mathf.Clamp(Count + amount, 0, MaxCount);

            ValueChange();
        }

        public bool Spend(int amount)
        {
            if (Count < amount)
            {
                return false;
            }
            else
            {
                Count -= amount;
                ValueChange();
                return true;

            }
        }

        public int SpendAll()
        {
            int count = Count;

            this.Count = 0;
            ValueChange();
            return count;
        }

        protected virtual void ValueChange() => OnValueChange?.Invoke(Count);

    }

}