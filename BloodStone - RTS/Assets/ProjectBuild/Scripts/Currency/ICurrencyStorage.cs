using Entity;
using System.Collections.Generic;
namespace Currency
{
    public interface ICurrencyStorage : IEntity
    {
        IReadOnlyList<ICurrency> Currencies { get; }

        void AddCurrencyByType(ICurrency typeCurrency, int amount);
        bool SpendCurrencyByType(ICurrency typeCurrency, int amount);
        ICurrency GetCurrencyByType(ICurrency typeCurrency);
        ICurrency GetFirstCurrency();
    } 
}