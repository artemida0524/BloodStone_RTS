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
            unit.Animator.Play(unit.IdleAnimation);
        }
    }
}