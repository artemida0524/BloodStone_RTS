using Game.Gameplay.Entity;
using Game.Gameplay.Stats;
using System;
using UnityEngine;

namespace Game.Gameplay
{
    public interface IHealth : IStat
    {
        bool IsMaxHealth { get; }

        void AddHealth(int amount);
        void SpendHealth(int amount);
    }

    public class Health : IHealth
    {
        public string Name { get; set; } = ResourceNames.HEALTH;
        public bool IsMaxHealth => Count >= MaxCount;

        public int MaxCount { get; protected set; }

        public int Count { get; protected set; }

        public event EventHandler OnDataChange;

        public Health()
        {
            
        }

        public Health(int count, int maxCount)
        {
            Count = count;
            MaxCount = maxCount;

            Count = Mathf.Clamp(Count, 0, MaxCount);
            OnDataChange?.Invoke(this, EventArgs.Empty);
        }

        public void AddHealth(int amount)
        {
            Count += amount;
            Count = Mathf.Clamp(Count, 0, MaxCount);
            OnDataChange?.Invoke(this, EventArgs.Empty);
        }

        public void SpendHealth(int amount)
        {
            Count -= amount;
            Count = Mathf.Clamp(Count, 0, MaxCount);
            OnDataChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public interface IHealthable
    {
        IHealth Health { get; }
    }

}