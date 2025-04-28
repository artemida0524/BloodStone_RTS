using Entity;
using Unit;

namespace State
{
    public class FollowState : MovableStateBase
    {
        private readonly UnitBase unit;
        private readonly IEntity entity;
        private readonly float radius;

        public FollowState(UnitBase unit, IEntity entity, float radius)
        {
            this.unit = unit;
            this.entity = entity;
            this.radius = radius;
        }

        public override void Enter()
        {
            SetDestinationAsyncRunner(unit, entity);
            unit.Animator.Play(unit.RunningAnimation);
        }

        public override void Update()
        {
            if ((entity.Position - unit.Position).magnitude < unit.Agent.stoppingDistance + radius)
            {
                unit.Agent.ResetPath();
                IsFinished = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
            unit.Agent.ResetPath();
        }
    }

}