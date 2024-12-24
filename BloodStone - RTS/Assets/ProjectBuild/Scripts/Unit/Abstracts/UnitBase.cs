using UnityEngine;
using Entity;
using UnityEngine.AI;
using State;

namespace Unit
{

    [SelectionBase, RequireComponent(typeof(NavMeshAgent))]
    public abstract class UnitBase : EntityBase, IUnit, IMovable, ISelectable
    {

        [field: SerializeField] public override Renderer BodyRenderer { get; protected set; }
        public override Vector3 Position => transform.position;
        public override float Radius => 0;

        [field: SerializeField] public InteractableUnits StateInteractable { get; protected set; }

        [Space]

        [SerializeField] protected GameObject objectSelect;

        public virtual string IdleAnimation { get; protected set; } = AnimationStateNames.IDLE;
        public virtual string WalkingAnimation { get; protected set; } = AnimationStateNames.WALKING;
        public virtual string RunningAnimation { get; protected set; } = AnimationStateNames.RUNNING;


        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }
        public bool CanMove { get; protected set; } = true;
        public bool IsSelection { get; protected set; } = false;
        public bool CanSelected { get; protected set; } = true;

        protected virtual void Awake()
        {
            Initialization();
            StateInteractable.Behaviour = InitializeState();
        }

        private void Update()
        {
            StateInteractable.UpdateState();
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
                objectSelect.SetActive(true);
                return true;
            }
            return false;
        }

        public virtual void Unselect()
        {
            IsSelection = false;
            objectSelect.SetActive(false);
        }

        public Vector2 GetScreenPosition()
        {
            return Camera.main.WorldToScreenPoint(transform.position);
        }
    }
}