namespace Currency
{
	public class Gold : CurrencyBase
	{
		public override int Count { get; protected set; } = 5252;
		public override int MaxCount { get; protected set; } = 10000;
	}

}