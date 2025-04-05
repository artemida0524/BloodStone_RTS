using Entity;
using Unit;
using UnityEngine;

namespace State
{
    public class AttackAndFollowState : StateBase
    {
        private readonly AttackingUnitBase unit;
        private readonly EntityBase targetEntity;

        private bool isMovingToTarget;

        public AttackAndFollowState(AttackingUnitBase unit, EntityBase targetEntity)
        {
            this.unit = unit;
            this.targetEntity = targetEntity;
        }

        public override void Enter()
        {
            TryMoveToTarget();
        }

        public override void Update()
        {
            if (targetEntity == null || unit == null)
                return;

            if (/*!unit.CurrentWeapon.AttackRightNow*/ true )
            {
                if (isMovingToTarget)
                {
                    TryMoveToTarget();

                    if (IsInAttackRange() && HasLineOfSight())
                    {
                        isMovingToTarget = false;
                        unit.ResetMove();
                    }
                }
                else
                {
                    if (ShouldFollowTarget())
                    {
                        TryMoveToTarget();
                        return;
                    }

                    unit.transform.LookAt(targetEntity.Position);
                    TryAttack();
                }

            }
            else
            {
                unit.transform.LookAt(targetEntity.Position);
            }

        }

        private void TryMoveToTarget()
        {
            if (ShouldFollowTarget())
            {
                unit.MoveTo(targetEntity.Position, targetEntity.Radius);
                isMovingToTarget = true;
            }
        }

        private void TryAttack()
        {
            if (unit.CanShoot())
            {
                Debug.Log("Attack");
                unit.Shoot(targetEntity);
            }
        }

        private bool IsInAttackRange()
        {
            float distance = Vector3.Distance(unit.Position, targetEntity.Position);
            return distance - targetEntity.Radius < unit.CurrentWeapon.Distance / 1.5f;
        }

        private bool HasLineOfSight()
        {
            return Physics.Raycast(new Ray(unit.Position, targetEntity.Position - unit.Position), out RaycastHit hit)
                   && hit.collider.gameObject.Equals(targetEntity.gameObject);
        }

        private bool ShouldFollowTarget()
        {
            float distance = Vector3.Distance(unit.Position, targetEntity.Position);
            bool isOutOfRange = distance - targetEntity.Radius > unit.CurrentWeapon.Distance * 1.5f;

            bool hasObstacle = Physics.Raycast(new Ray(unit.Position, targetEntity.Position - unit.Position), out RaycastHit hit)
                               && !hit.collider.gameObject.Equals(targetEntity.gameObject);

            return isOutOfRange || hasObstacle;
        }
    }
}
