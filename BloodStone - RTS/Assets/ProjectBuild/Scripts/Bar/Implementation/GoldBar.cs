using System;
using Currency;


namespace Bar
{
    public class GoldBar : IBar
    {
        public string Name { get; private set; } = "Gold";
        public int MaxCount { get; private set; }
        public int Count { get; private set; }

        public Action OnDataChange { get; set; }

        private Gold gold;

        public GoldBar(Gold gold)
        {
            this.gold = gold;

            this.MaxCount = gold.MaxCount;
            this.Count = gold.Count;

            gold.OnValueChange += OnChange;
        }

        public void OnChange(int count)
        {
            this.Count = count;
            OnDataChange?.Invoke();
        }

        public void Dispose()
        {
            gold.OnValueChange -= OnChange;
        }
    }



}