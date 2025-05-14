using System;
using Unit;


namespace Bar
{
    public class HealthBar : IStats
    {
        public string Name { get; private set; } = "Health";
        public int MaxCount { get; private set; }
        public int Count { get; private set; }

        public event Action OnDataChange;

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