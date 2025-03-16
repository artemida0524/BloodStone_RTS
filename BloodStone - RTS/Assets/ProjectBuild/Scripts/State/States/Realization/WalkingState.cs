using Entity;
using Unit;
using UnityEngine;

namespace State
{
    public class WalkingState : MovableStateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 point;

        public WalkingState(UnitBase unit, EntityBase entity)
        {
            this.unit = unit;
            this.point = entity.Position;
        }

        public WalkingState(UnitBase unit, Vector3 point)
        {
            this.unit = unit;
            this.point = point;
        }

        public override void Enter()
        {
            unit.Animator.Play(unit.WalkingAnimation);
            SetDestinationAsyncRunner(unit, point);
        }

        public override void Update()
        {
            if ((point - unit.Position).sqrMagnitude < unit.Agent.stoppingDistance)
            {
                unit.Agent.ResetPath();
                IsFinished = true;
            }
                    
        }



        public override void Exit()
        {
            base.Exit();
        }

    }
}