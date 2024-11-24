using UnityEngine;
using Entity;
using UnityEngine.AI;
using State;
using System;
using UnityEditor.Search;

namespace Unit
{
    [SelectionBase, RequireComponent(typeof(NavMeshAgent))]
    public abstract class UnitBase : EntityBase, IUnit, IMovable
    {
        [SerializeField] protected StateInteractable stateInteractable;
        [SerializeField] private GameObject target;
        [SerializeField] private LayerMask layerMask;

        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }
        public bool CanMove { get; protected set; } = true;

        protected void Awake()
        {
            Initialization();

            //InitializeState(stateInteractable.Behaviour);

            stateInteractable.Behaviour = InitializeState();
        }

        private void Update()
        {

            stateInteractable.UpdateState();

            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, layerMask))
                {
                    MoveTo(hitInfo.point);

                    target.transform.position = hitInfo.point;
                }
            }
        }


        protected abstract StateBehaviourBase InitializeState();

        protected virtual void Initialization()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
        }

        public virtual void MoveTo(Vector3 point)
        {
            stateInteractable.SetState(new WalkingState(this, point));
        }
    }
}