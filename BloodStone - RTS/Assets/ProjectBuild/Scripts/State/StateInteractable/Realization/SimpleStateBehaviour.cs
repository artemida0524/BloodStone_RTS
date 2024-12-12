using Unit;
using UnityEngine;

namespace State
{

    public class SimpleStateBehaviour : StateBehaviourBase
    {
        private readonly SimpleUnitBase unit;

        public SimpleStateBehaviour(SimpleUnitBase unit)
        {
            this.unit = unit;
            base.StateMachine = new StateMachine(new IdleState(unit));
        }

        public override void Interaction(Collider collision)
        {
        }

        public override void SetState(StateBase state)
        {
            Debug.Log("change");

            StateMachine.ChangeState(state);
        }

        public override void SetStateIfFinished()
        {
            StateMachine.ChangeState(new IdleState(unit));
        }
    }
}
