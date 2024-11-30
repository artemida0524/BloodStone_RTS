using Unit;
using UnityEngine;

namespace State
{
    public class IdleState : StateBase
    {
        private readonly UnitBase unit;
        public IdleState(UnitBase unit)
        {
            this.unit = unit;
        }

        public override void Enter()
        {
            unit.Animator.Play(AnimationStateNames.IDLE);

        }


        //public override void Exit()
        //{
        //    Debug.Log("Exit idle state");
        //}

    }

}