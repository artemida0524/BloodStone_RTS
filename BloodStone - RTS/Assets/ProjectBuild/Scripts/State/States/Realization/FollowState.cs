using Entity;
using Unit;

namespace State
{
    public class FollowState : MovableStateBase
    {
        private readonly UnitBase _unit;
        private readonly IEntity _entity;
        private readonly float _radius;

        public FollowState(UnitBase unit, IEntity entity, float radius)
        {
            _unit = unit;
            _entity = entity;
            _radius = radius;
        }

        public override void Enter()
        {
            SetDestinationAsyncRunner(_unit, _entity);
            _unit.Animator.Play(_unit.RunningAnimation);
        }

        public override void Update()
        {
            if ((_entity.Position - _unit.Position).magnitude < _unit.Agent.stoppingDistance + _radius)
            {
                _unit.Agent.ResetPath();
                IsFinished = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
            _unit.Agent.ResetPath();
        }
    }

}