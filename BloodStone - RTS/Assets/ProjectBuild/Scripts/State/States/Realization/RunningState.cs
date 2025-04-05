using Unit;
using UnityEngine;

namespace State
{
    public class RunningState : MovableStateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 point;
        private readonly float radius;
        private readonly bool automaticIdleAnimation;


        private readonly float beginSpeed;
        public RunningState(UnitBase unit, Vector3 point, float radius, bool automaticIdleAnimation = true)
        {
            this.unit = unit;
            this.point = point;
            this.radius = radius;
            this.automaticIdleAnimation = automaticIdleAnimation;

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
            Debug.Log("run");
            if ((point - unit.Position).magnitude < unit.Agent.stoppingDistance + radius)
            {
                unit.Agent.ResetPath();
                if (automaticIdleAnimation)
                {
                    unit.Animator.Play(unit.IdleAnimation);
                }
                IsFinished = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
            unit.Agent.speed = beginSpeed;
            unit.Agent.ResetPath();
        }

    }
}