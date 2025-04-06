using State;
using System;
using UnityEngine;

namespace State
{
    [Serializable]
    public class InteractableUnits
    {
        public StateBehaviourBase Behaviour { get; private set; }
        public StateMachine MoveState { get; private set; } = new StateMachine(null);
        [field: SerializeField] public float Radius { get; private set; } = 5f;

        public void Update()
        {
            UpdateState();
            if (Behaviour.StateMachine.State != null && Behaviour.StateMachine.State.IsFinished)
            {
                Behaviour.SetStateIfFinished();
            }
        }

        public void Init(StateBehaviourBase stateBehaviour)
        {
            Behaviour = stateBehaviour;
        }

        public void SetState(StateBase state)
        {
            Behaviour.SetState(state);
        }

        private void UpdateState()
        {
            Behaviour.StateMachine?.Update();
            MoveState.Update();
        }
    }
}