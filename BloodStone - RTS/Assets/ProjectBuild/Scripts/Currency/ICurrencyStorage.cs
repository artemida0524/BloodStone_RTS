using Game.Gameplay.Entity;
using System.Collections.Generic;
namespace Currency
{
    public interface ICurrencyStorage : IEntity
    {
        IReadOnlyList<ICurrency> Currencies { get; }

        void AddCurrencyByName(string name, int amount);
        bool SpendCurrencyByName(string name, int amount);
        ICurrency GetCurrencyByName(string name);
        ICurrency GetFirstCurrency();
    } 
}