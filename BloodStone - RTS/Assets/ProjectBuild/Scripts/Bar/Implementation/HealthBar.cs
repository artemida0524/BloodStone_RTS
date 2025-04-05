using System;
using Unit;


namespace Bar
{
    public class HealthBar : IBar
    {
        public string Name { get; private set; } = "Health";
        public int MaxCount { get; private set; }
        public int Count { get; private set; }

        public Action OnDataChange { get; set; }

        private IHealth health;

        public HealthBar(IHealth health)
        {
            this.health = health;

            this.MaxCount = health.MaxCountHealth;
            this.Count = health.CountHealth;

            health.OnHealthChange += OnChange;
        }

        public void OnChange(int count)
        {
            this.Count = count;
            OnDataChange?.Invoke();
        }

        public void Dispose()
        {
            health.OnHealthChange -= OnChange;
        }
    }
}