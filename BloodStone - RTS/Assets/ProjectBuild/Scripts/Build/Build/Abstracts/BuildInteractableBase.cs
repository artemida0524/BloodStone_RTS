using System.Collections.Generic;
using UnityEngine;
using Unit;
using Select;
using Option;
using Interaction;
using System;

namespace Build
{

    public abstract class BuildInteractableBase : BuildBase, IInteractable, ISelectable, IHoverable, IInteractableSelectables, IDamageable, IHealth
    {
        [field: SerializeField] private GameObject selectObject;

        public bool IsSelection { get; private set; } = false;
        public bool CanSelected { get; private set; } = true;
        public virtual IOption Options { get; protected set; }

        [field: SerializeField] public int MaxCountHealth { get; protected set; } = 100;
        [field: SerializeField] public int CountHealth { get; protected set; }

        public bool IsMaxHealth => CountHealth >= MaxCountHealth;

        public event Action<int> OnTakeDamage;
        public event Action<int> OnHealthChange;
        public event Action<IReadOnlyList<ISelectable>> OnInteractWithSelectables;


        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void Start()
        {
            Options = InitOption();
        }

        protected virtual IOption InitOption()
        {
            return new OptionBase(this);
        }

        public virtual void Interact()
        {

        }

        public void AddHealth(int amount)
        {
            CountHealth += amount;
            CountHealth = Mathf.Clamp(CountHealth, 0, MaxCountHealth);
            OnHealthChange?.Invoke(CountHealth);
        }

        public void SpendHealth(int amount)
        {
            CountHealth -= amount;
            CountHealth = Mathf.Clamp(CountHealth, 0, MaxCountHealth);
            OnHealthChange?.Invoke(CountHealth);
        }

        public virtual bool Select()
        {
            if (CanSelected)
            {
                IsSelection = true;
                selectObject.SetActive(true);
                return true;
            }
            return false;
        }

        public virtual void Unselect()
        {
            IsSelection = false;
            selectObject.SetActive(false);
        }
        public virtual void Hover()
        {
        }

        public virtual void Unhover()
        {
        }

        public virtual void Interact(IReadOnlyList<ISelectable> selectables)
        {
            OnInteractWithSelectables?.Invoke(selectables);
        }

        public void TakeDamage(int amount)
        {
            SpendHealth(amount);
            OnTakeDamage?.Invoke(amount);
        }
    }
}