using UnityEngine;
using Entity;
using UnityEngine.AI;
using State;
using Data;
using Faction;

namespace Unit
{

    [SelectionBase, RequireComponent(typeof(NavMeshAgent))]
    public abstract class UnitBase : EntityBase, IUnit, IMovable, ISelectable
    {

        [field: SerializeField] public override Renderer Renderer { get; protected set; }

        [SerializeField] protected InteractableUnits stateInteractable;

        [Space]

        [SerializeField] protected GameObject objectSelect;

        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }
        public bool CanMove { get; protected set; } = true;
        public bool IsSelection { get; protected set; } = false;
        public bool CanSelected { get; protected set; } = true;

        protected void Awake()
        {
            Initialization();

            stateInteractable.Behaviour = InitializeState();
        }

        private void Update()
        {

            stateInteractable.UpdateState();

            //if (Input.GetMouseButton(0))
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //    if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, layerMask))
            //    {
            //        MoveTo(hitInfo.point);
            //    }
            //}
        }

        protected abstract StateBehaviourBase InitializeState();

        public override void Initialization(FactionType type, EntityCollectionData collectionData)
        {
            FactionType = type;
            collectionData.SetUnit(this);
        }

        protected virtual void Initialization()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
        }

        public virtual void MoveTo(Vector3 point)
        {
            if (CanMove)
            {
                stateInteractable.SetState(new WalkingState(this, point));
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