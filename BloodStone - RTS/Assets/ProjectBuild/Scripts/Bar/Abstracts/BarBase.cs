using Game.Gameplay.Stats;
using System;


namespace Bar
{
    public class BarBase : IBar
    {
        public string Name { get; protected set; }

        public int MaxCount => stats.MaxCount;

        public int Count => stats.Count;

        public event EventHandler OnDataChange;

        protected IStat stats;

        public BarBase(IStat stats, string name)
        {
            this.stats = stats;
            Name = name;

            stats.OnDataChange += OnDataChangeHandler;
        }

        private void OnDataChangeHandler(object sender, EventArgs e)
        {
            OnDataChange?.Invoke(sender, e);
        }

        public void Dispose()
        {
            stats.OnDataChange -= OnDataChangeHandler;
        }
    }
}