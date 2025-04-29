using Unit;
using UnityEngine;

namespace State
{
    public abstract class AttackingStateBehaviourBase : StateBehaviourBase
    {
        private readonly AttackingUnitBase unit;

        public AttackingStateBehaviourBase(AttackingUnitBase unit)
        {
            this.unit = unit;
            StateMachine = new StateMachine(new PatroolIdleState(unit));
        }

        public override void Interaction(Collider collision)
        {

        }

        public override void SetState(StateBase state)
        {
            StateMachine.ChangeState(state);
        }

        public override void SetStateIfFinished()
        {
            StateMachine.ChangeState(new PatroolIdleState(unit));
        }

    }
}
