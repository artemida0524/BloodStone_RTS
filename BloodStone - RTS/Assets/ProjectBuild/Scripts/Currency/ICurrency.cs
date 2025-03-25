using System;

namespace Currency
{
    public interface ICurrency
    {
        int Count { get; }
        int MaxCount { get; }
        event Action<int> OnValueChange;


        void Add(int amount);
        bool Spend(int amount);

    } 
}
