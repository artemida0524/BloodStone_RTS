using Game.Gameplay;
using Game.Gameplay.Entity;
using Game.Gameplay.Units;
using Game.Gameplay.Units.Utils;
using System;
using UnityEngine;

namespace State
{
    public class AttackAndFollowState : StateBase
    {
        private readonly AttackingUnitBase _unit;
        private readonly IDamageable _entity;

        private StateMachine _machine;

        private Func<bool> IsObstacleDetected;

        public AttackAndFollowState(AttackingUnitBase unit, IDamageable targetEntity)
        {
            _unit = unit;
            _entity = targetEntity;

            IsObstacleDetected = ObstacleDetected;
        }

        public override void Enter()
        {
            _machine = new StateMachine(new MoveToEntityState(_unit, _entity, IsObstacleDetected));

            UnitUtility.OnUnitDisableOrDestroy += OnUnitDisableOrDestroyHandler;
        }

        private void OnUnitDisableOrDestroyHandler(UnitBase obj)
        {
            if (obj != null && _entity.Equals(obj))
            {
                IsFinished = true;
                _machine.ChangeState(null);
            }
        }

        public override void Update()
        {
            if (_entity == null || _unit == null)
            {
                IsFinished = true;
                return;
            }

            Debug.Log(IsFinished);

            _machine.Update();

            if (_machine.State.IsFinished)
            {
                switch (_machine.State)
                {
                    case MoveToEntityState:
                        _machine.ChangeState(new AttackState(_unit, _entity, IsObstacleDetected));
                        break;

                    case AttackState:
                        _machine.ChangeState(new MoveToEntityState(_unit, _entity, IsObstacleDetected));
                        break;
                }
            }
        }

        public override void Exit()
        {
            Debug.Log("Exit");
            UnitUtility.OnUnitDisableOrDestroy -= OnUnitDisableOrDestroyHandler;
        }

        private bool ObstacleDetected()
        {
            if (Physics.Raycast(new Ray(_unit.Position, _entity.Position - _unit.Position), out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out IEntity entity))
                {
                    if (!_entity.Equals(entity))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private class MoveToEntityState : StateBase
        {
            private readonly AttackingUnitBase _unit;
            private readonly IDamageable _entity;

            private Func<bool> IsObstacleDetected;

            public MoveToEntityState(AttackingUnitBase unit, IDamageable entity, Func<bool> IsObstacleDetected)
            {
                _unit = unit;
                _entity = entity;
                this.IsObstacleDetected = IsObstacleDetected;
            }

            public override void Enter()
            {
                base.Enter();
                _unit.MoveTo(new FollowState(_unit, _entity, _entity.Radius));
            }

            public override void Update()
            {
                base.Update();
                if (Vector3.Distance(_unit.Position, _entity.Position) < (_unit.CurrentWeapon.Distance * 0.8f) + _entity.Radius && !IsObstacleDetected())
                {
                    IsFinished = true;
                }
            }

            public override void Exit()
            {
                base.Exit();
                _unit.ResetMove();
            }
        }

        private class AttackState : StateBase
        {
            private readonly AttackingUnitBase _unit;
            private readonly IDamageable _entity;

            private bool _attackRightNow = false;

            private Func<bool> IsObstacleDetected;

            public AttackState(AttackingUnitBase unit, IDamageable entity, Func<bool> IsObstacleDetected)
            {
                _unit = unit;
                _entity = entity;
                this.IsObstacleDetected = IsObstacleDetected;
            }

            public override void Enter()
            {
                base.Enter();
                _unit.Animator.Play(_unit.IdleAnimation);

                _unit.AnimationEventCallBalck.OnBeginAttack += OnBeginAttackHandler;
                _unit.AnimationEventCallBalck.OnEndAttack += OnEndAttackHandler;
            }

            public override void Update()
            {
                base.Update();

                float distance = Vector3.Distance(_unit.Position, _entity.Position) - _entity.Radius;

                if ((distance > _unit.CurrentWeapon.Distance || IsObstacleDetected()) && !_attackRightNow)
                {
                    IsFinished = true;
                }

                if (_unit.CurrentWeapon.CanShoot())
                {
                    _unit.Shoot(_entity as EntityBase);
                }

                _unit.transform.LookAt(_entity.Position);
            }

            public override void Exit()
            {
                base.Exit();

                _unit.AnimationEventCallBalck.OnBeginAttack -= OnBeginAttackHandler;
                _unit.AnimationEventCallBalck.OnEndAttack -= OnEndAttackHandler;
            }

            private void OnBeginAttackHandler()
            {
                _attackRightNow = true;
            }

            private void OnEndAttackHandler()
            {
                _attackRightNow = false;
            }
        }
    }
}