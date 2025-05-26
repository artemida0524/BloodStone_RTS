using Game.Gameplay.Stats;
using System;

namespace Currency
{
    public interface ICurrency : IStat
    {
        string Name { get; }
        bool IsFull { get; }

        void Add(int amount);
        bool Spend(int amount);
        int SpendAll();

    } 
}
