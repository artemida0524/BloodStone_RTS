using System.Collections.Generic;
namespace Currency
{
    public interface ICurrencyStorage
    {
        IReadOnlyList<ICurrency> Currencies { get; }

        void AddCurrency<T>(int amount) where T : ICurrency;
        bool SpendCurrency<T>(int amount) where T : ICurrency;

        ICurrency GetCurrency<T>() where T : ICurrency;
        ICurrency GetFirstCurrency();
    } 
}
