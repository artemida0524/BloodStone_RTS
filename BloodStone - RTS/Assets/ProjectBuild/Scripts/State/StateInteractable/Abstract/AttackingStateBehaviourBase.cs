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
            if (collision.gameObject.TryGetComponent(out UnitBase unit))
            {
                ////if (unit)
                ////{
                //    SetState(new AttackAndFollowState(this.unit, unit));
                ////}
            }
        }

        public override void SetState(StateBase state)
        {
            //must be changed
            //if(state is WalkingState)
            //{
            //    StateMachine.ChangeState(state);
            //}
            //else if (state is AttackAndFollowState)
            //{
            //    Debug.Log("idle");
            //    if(StateMachine.State is IdleState)
            //    {
            //        StateMachine.ChangeState(state);
            //    }                
            //}


            //if(state is AttackAndFollowState)
            //{
                StateMachine.ChangeState(state);
            //}

        }

        public override void SetStateIfFinished()
        {
            StateMachine.ChangeState(new PatroolIdleState(unit));
        }

    }
}
