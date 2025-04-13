using Unit;
using UnityEngine;

namespace State
{
    public class RunningState : MovableStateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 point;
        private readonly float radius;


        private readonly float beginSpeed;
        public RunningState(UnitBase unit, Vector3 point, float radius)
        {
            this.unit = unit;
            this.point = point;
            this.radius = radius;

            this.beginSpeed = unit.Agent.speed;
        }

        public override void Enter()
        {
            SetDestinationAsyncRunner(unit, point);
            unit.Animator.Play(unit.RunningAnimation);
            unit.Agent.speed = 10f;
        }

        public override void Update()
        {
            if ((point - unit.Position).magnitude < unit.Agent.stoppingDistance + radius)
            {
                unit.Agent.ResetPath();
                IsFinished = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
            unit.Agent.speed = beginSpeed;
        }

    }
}