using System;
using UnityEngine;

namespace Currency
{
    public class CurrencyBase : ICurrency
    {
        public string Name { get; protected set; } = "Default";

        public virtual int Count { get; protected set; } = 1000;
        public virtual int MaxCount { get; protected set; } = 10000;

        public bool IsFull => Count >= MaxCount;

        public event EventHandler OnDataChange;


        public CurrencyBase(string name)
        {
            Name = name;
        }

        public CurrencyBase(string name, int count, int maxCount) : this(name)
        {
            MaxCount = maxCount;
            Count = count;
            Count = Mathf.Clamp(Count, 0, MaxCount);
        }

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

        protected virtual void ValueChange() => OnDataChange?.Invoke(this, EventArgs.Empty);

    }

}