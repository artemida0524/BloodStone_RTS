using Game.Gameplay.Build;
using Game.Gameplay.Units;
using System;
using UnityEngine;

namespace State
{
    public class BuildWorkerState : StateBase
    {
        private readonly BuilderWorkerUnit _unit;
        private readonly BuildInteractableBase _build;

        private StateMachine _machine = new StateMachine();

        private const float TIME_TO_WALK_AROUND = 7f;
        private float _timeOut = 0f;

        public BuildWorkerState(BuilderWorkerUnit unit, BuildInteractableBase build)
        {
            _unit = unit;
            _build = build;
        }

        public override void Enter()
        {
            base.Enter();
            _machine.ChangeState(new WorkState(_unit, _build));
            _build.Health.OnDataChange += OnHealthChangeHandler;
        }

        private void OnHealthChangeHandler(object sender, EventArgs e)
        {
            if (_build.Health.IsMaxHealth)
            {
                IsFinished = true;
            }
        }

        public override void Update()
        {
            base.Update();
            _machine.Update();

            if (_machine.State.IsFinished)
            {
                switch (_machine.State)
                {
                    case GoToPoint:
                        _machine.ChangeState(new WorkState(_unit, _build));
                        break;
                }
            }

            _timeOut += Time.deltaTime;

            if (_timeOut > TIME_TO_WALK_AROUND)
            {
                _machine.ChangeState(new GoToPoint(_unit, _build));
                _timeOut = 0f;
            }

        }

        private class WorkState : StateBase
        {
            private readonly BuilderWorkerUnit _unit;
            private readonly BuildInteractableBase _build;

            public WorkState(BuilderWorkerUnit unit, BuildInteractableBase build)
            {
                _unit = unit;
                _build = build;
            }

            public override void Enter()
            {
                base.Enter();
                _unit.Animator.Play(AnimationStateNames.CHOP_TREE);
                _unit.AnimationEventCallBack.OnChopTree += OnChopTree;
            }

            private void OnChopTree()
            {
                _build.AddHealth(5);
            }
        }

        private class GoToPoint : StateBase
        {
            private readonly BuilderWorkerUnit _unit;
            private readonly BuildInteractableBase _build;
            public GoToPoint(BuilderWorkerUnit unit, BuildInteractableBase build)
            {

                _unit = unit;
                _build = build;
            }

            public override void Enter()
            {
                base.Enter();

                Vector2 point = UnityEngine.Random.insideUnitCircle * _build.Radius;
                _unit.MoveTo(new Vector3(_build.Position.x + point.x, _build.Position.y, _build.Position.z + point.y), 1);
            }

            public override void Update()
            {
                base.Update();

                if (_unit.StateInteractable.MoveState.State.IsFinished)
                {
                    IsFinished = true;
                }
            }

        }
    }
}