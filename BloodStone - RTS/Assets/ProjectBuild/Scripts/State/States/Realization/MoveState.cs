using Unit;
using UnityEditor.Rendering;
using UnityEngine;

namespace State
{
    public class MoveState : StateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 target;
        private readonly float radius;
        public MoveState(UnitBase unit, Vector3 target, float radius)
        {
            this.unit = unit;
            this.target = target;
            this.radius = radius;
        }

        public override void Enter()
        {
            unit.ResetMove();
            unit.MoveTo(target, radius);
        }

        public override void Update()
        {
            if (unit.StateInteractable.MoveState.State.IsFinished)
            {

                unit.Animator.Play(unit.IdleAnimation);

                unit.ResetMove();
                IsFinished = true;
            }
        }

        public override void Exit()
        {
            unit.ResetMove();
        }
    }
}