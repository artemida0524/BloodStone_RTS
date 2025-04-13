namespace Currency
{
    public class Gold : CurrencyBase
    {
        public override int Count { get; protected set; } = 1000;
        public override int MaxCount { get; protected set; } = 10000;
    }

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