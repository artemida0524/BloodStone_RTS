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
            //Debug.Log(state + " " + StateMachine.State);

            //if (state is AttackAndFollowState attackAndFollowState && StateMachine.State is AttackAndFollowState attackAndFollowState2)
            //{
            //    if (attackAndFollowState.targetEntity.Equals(attackAndFollowState2.targetEntity))
            //    {
            //        return;
            //    }
            //}

            //if (state is FollowState followState && StateMachine.State is FollowState followState2)
            //{
            //    if (followState.targetEntity.Equals(followState2.targetEntity))
            //    {
            //        return;
            //    }
            //}


            //if (state is AttackAndFollowState qwer && StateMachine.State is FollowState qwerty)
            //{
            //    if (qwer.targetEntity.Equals(qwerty.targetEntity))
            //    {
            //        return;
            //    }
            //}

            StateMachine.ChangeState(state);

        }

        public override void SetStateIfFinished()
        {
            StateMachine.ChangeState(new PatroolIdleState(unit));
        }

    }
}
