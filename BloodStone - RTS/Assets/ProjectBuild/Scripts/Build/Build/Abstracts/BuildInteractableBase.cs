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

        public event Action<int> OnTakeDamage;
        public event Action<int> OnHealthChange;

        public bool IsSelection { get; private set; } = false;
        public bool CanSelected { get; private set; } = true;
        public virtual IOption Options { get; protected set; }

        public int MaxCountHealth { get; protected set; }
        public int CountHealth { get; protected set; }


        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void Start()
        {
            Options = InitOption();

        }

        protected override void Update()
        {

        }

        protected virtual IOption InitOption()
        {
            return new OptionBase(this);
        }

        public virtual void Interact()
        {

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
            
        }

        public void TakeDamage(int amount)
        {
            CountHealth -= amount;
            OnHealthChange?.Invoke(CountHealth);
            OnTakeDamage?.Invoke(amount);
        }
    }
}