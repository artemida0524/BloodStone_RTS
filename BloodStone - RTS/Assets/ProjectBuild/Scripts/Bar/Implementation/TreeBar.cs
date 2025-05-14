using System;
using Currency;


namespace Bar
{
    public class TreeBar : IStats
    {
        public string Name { get; private set; } = "Tree";
        public int MaxCount { get; private set; }
        public int Count { get; private set; }

        public event Action OnDataChange;

        private TreeCurrency tree;

        public TreeBar(TreeCurrency tree)
        {
            this.tree = tree;

            this.MaxCount = tree.MaxCount;
            this.Count = tree.Count;

            tree.OnValueChange += OnChange;
        }

        private void OnChange(int count)
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