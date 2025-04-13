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

    public class TreeBar : IBar
    {
        public string Name { get; private set; } = "Tree";
        public int MaxCount { get; private set; }
        public int Count { get; private set; }

        public Action OnDataChange { get; set; }

        private TreeCurrency tree;

        public TreeBar(TreeCurrency tree)
        {
            this.tree = tree;

            this.MaxCount = tree.MaxCount;
            this.Count = tree.Count;

            tree.OnValueChange += OnChange;
        }

        public void OnChange(int count)
        {
            this.Count = count;
            OnDataChange?.Invoke();
        }

        public void Dispose()
        {
            tree.OnValueChange -= OnChange;
        }
    }

}