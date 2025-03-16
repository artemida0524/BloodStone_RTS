using UnityEngine;
using Entity;
using UnityEngine.AI;
using State;
using System;
using Bar;
using GlobalData;
using Select;
using Unity.VisualScripting;
using Option;

namespace Unit
{
    [SelectionBase, RequireComponent(typeof(NavMeshAgent))]
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

        public virtual string IdleAnimation { get; protected set; } = AnimationStateNames.IDLE;
        public virtual string WalkingAnimation { get; protected set; } = AnimationStateNames.WALKING;
        public virtual string RunningAnimation { get; protected set; } = AnimationStateNames.RUNNING;

        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }
        public bool CanMove { get; protected set; } = true;
        public bool IsSelection { get; protected set; } = false;
        public bool CanSelected { get; protected set; } = true;


        public abstract Sprite Sprite { get; protected set; }

        public IInteractable SelectInfo { get; protected set; }

        public event Action<int> OnHealthChange;

        protected virtual void Awake()
        {
            Initialization();
            StateInteractable.Behaviour = InitializeState();

            UIBarContainer?.AddBar(new HealthBar(this));


            SelectInfo = new InteractableUnit(this);
        }

        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {
            StateInteractable.Update();
        }

        public virtual void OnEnable()
        {
            GlobalUnitsDataHandler.AddUnit(this);
        }

        public virtual void OnDisable()
        {
            GlobalUnitsDataHandler.RemoveUnit(this);
        }

        public virtual void OnDestroy()
        {
            GlobalUnitsDataHandler.RemoveUnit(this);

            

        }

        protected abstract StateBehaviourBase InitializeState();

        protected virtual void Initialization()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
        }

        public virtual void MoveTo(Vector3 point)
        {
            if (CanMove)
            {
                StateInteractable.SetState(new WalkingState(this, point));
            }
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

        public virtual void TakeDamage(int amount)
        {
            CountHealth -= amount;
            OnHealthChange?.Invoke(CountHealth);
        }

        public virtual void Hover()
        {
            UIBarContainer?.gameObject.SetActive(true);
        }

        public void Unhover()
        {
            UIBarContainer?.gameObject.SetActive(false);
        }


        public void DoSomething()
        {
            StateInteractable.SetState(new WalkingState(this, FindObjectOfType<Build.Faction>().Position));
            Debug.Log(FindObjectOfType<Build.Faction>().Position);
        }

    }
}