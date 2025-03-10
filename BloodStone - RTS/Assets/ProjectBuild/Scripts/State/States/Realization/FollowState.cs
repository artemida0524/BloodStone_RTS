using Entity;
using Unit;
using UnityEngine;

namespace State
{
    public class FollowState : MovableStateBase
    {
        public readonly AttackingUnitBase unit;
        public readonly EntityBase targetEntity;
        private readonly StateBase changedState;

        public FollowState(AttackingUnitBase unit, EntityBase targetEntity, StateBase changedState)
        {
            this.unit = unit;
            this.targetEntity = targetEntity;
            this.changedState = changedState;
        }

        public override void Enter()
        {
            SetDestinationAsyncRunner(unit, targetEntity, 500);



            unit.Animator.Play(unit.RunningAnimation);
            unit.Agent.speed = 6f;
        }

        public override void Update()
        {
            Debug.DrawRay(unit.Position, targetEntity.Position - unit.Position);

            if (Vector3.Distance(unit.Position, targetEntity.Position) - targetEntity.Radius < unit.Weapon.Distance / 1.5f)
            {
                if (Physics.Raycast(new Ray(unit.Position, targetEntity.Position - unit.Position), out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.Equals(targetEntity.gameObject))
                    {
                        unit.StateInteractable.SetState(changedState);
                    }
                }
            }
        }

        public override void Exit()
        {
            Debug.Log("Exit");
            base.Exit();
            unit.Animator.Play(unit.IdleAnimation);
            unit.Agent.ResetPath();

        }
    }
}




