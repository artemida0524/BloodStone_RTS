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
            //    if (attackAndFollowState._entity.Equals(attackAndFollowState2._entity))
            //    {
            //        return;
            //    }
            //}

            //if (state is FollowWithChangeState followState && StateMachine.State is FollowWithChangeState followState2)
            //{
            //    if (followState._entity.Equals(followState2._entity))
            //    {
            //        return;
            //    }
            //}


            //if (state is AttackAndFollowState qwer && StateMachine.State is FollowWithChangeState qwerty)
            //{
            //    if (qwer._entity.Equals(qwerty._entity))
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
