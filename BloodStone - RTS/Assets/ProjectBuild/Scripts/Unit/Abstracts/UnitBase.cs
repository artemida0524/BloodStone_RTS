using UnityEngine;
using Entity;
using UnityEngine.AI;
using State;
using System;
using Bar;
using Select;
using Option;

namespace Unit
{
    [SelectionBase, RequireComponent(typeof(NavMeshAgent), typeof(CapsuleCollider), typeof(Rigidbody))]
    public abstract class UnitBase : EntityBase, IUnit, IMovable, ISelectable, IHealth, IDamageable, IHoverable
    {
        [field: SerializeField] public override Renderer BodyRenderer { get; protected set; }
        [field: SerializeField] public UIBarContainer UIBarContainer { get; private set; }

        public override Vector3 Position => transform.position;
        public override float Radius => 0;

        [field: SerializeField] public InteractableUnits StateInteractable { get; protected set; } = new InteractableUnits();

        [SerializeField] protected GameObject selectObject;

        [field: SerializeField] public int MaxCountHealth { get; protected set; } = 100;
        [field: SerializeField] public int CountHealth { get; protected set; } = 100;

        public bool IsMaxHealth => CountHealth >= MaxCountHealth;

        public virtual string IdleAnimation { get; protected set; } = AnimationStateNames.IDLE;
        public virtual string WalkingAnimation { get; protected set; } = AnimationStateNames.WALKING;
        public virtual string RunningAnimation { get; protected set; } = AnimationStateNames.RUNNING;

        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }
        public bool CanMove { get; protected set; } = true;
        public bool IsSelection { get; protected set; } = false;
        public bool CanSelected { get; protected set; } = true;

        public IOption Options { get; protected set; }

        public event Action<int> OnHealthChange;
        public event Action<int> OnTakeDamage;
        public event Action<StateBase> OnStateChange;

        protected virtual void Awake()
        {
            Initialization();
            StateInteractable.Init(InitializeState());

            UIBarContainer?.AddBar(new HealthBar(this));
        }

        protected virtual void Start()
        {
            Options = InitOption();
        }

        protected virtual void Update()
        {
            StateInteractable.Update();
        }

        protected virtual void OnEnable()
        {
            UnitUtility.OnUnitEnableInvoke(this);
        }

        protected virtual void OnDisable()
        {
            UnitUtility.OnUnitDisableOrDestroyInvoke(this);
        }

        protected virtual void OnDestroy()
        {
            UnitUtility.OnUnitDisableOrDestroyInvoke(this);
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    StateInteractable.Behaviour.Interact(collision.collider);
        //}

        protected abstract StateBehaviourBase InitializeState();

        protected virtual void Initialization()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
        }

        public void SetState(StateBase state)
        {
            StateInteractable.SetState(state);
            OnStateChange?.Invoke(state);
        }

        public virtual bool MoveTo(Vector3 point, float radius)
        {
            if (CanMove)
            {
                StateInteractable.MoveState.ChangeState(new WalkingState(this, point, radius));
            }
            return CanMove;
        }

        public void ResetMove()
        {
            StateInteractable.MoveState.ChangeState(null);
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

        public void TakeDamage(int amount)
        {
            SpendHealth(amount);
            OnTakeDamage?.Invoke(amount);
        }

        public virtual void Hover()
        {
            UIBarContainer?.gameObject.SetActive(true);
        }

        public void Unhover()
        {
            UIBarContainer?.gameObject.SetActive(false);
        }

        public virtual IOption InitOption()
        {
            return new OptionUnitBase(this);
        }

        public void DoSomething()
        {
            SetState(new MoveState(this, FindObjectOfType<Build.Faction>().Position, FindObjectOfType<Build.Faction>().Radius));
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

    }
}