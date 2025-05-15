namespace Currency
{
    public class TreeCurrency : CurrencyBase
    {

        public TreeCurrency()
        {

        }

        public TreeCurrency(int amount, int maxCount)
        {
            MaxCount = maxCount;
            Count = amount;
        }
    }

}