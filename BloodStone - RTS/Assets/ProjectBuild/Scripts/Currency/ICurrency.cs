using System;

namespace Currency
{
    public interface ICurrency
    {
        int Count { get; }
        int MaxCount { get; }

        bool IsFull { get; }

        event Action<int> OnValueChange;

        void Add(int amount);
        bool Spend(int amount);
        int SpendAll();

    } 
}
