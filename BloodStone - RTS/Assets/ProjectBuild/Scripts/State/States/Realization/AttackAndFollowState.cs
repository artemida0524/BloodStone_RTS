using Entity;
using Unit;
using UnityEngine;

namespace State
{
    
    //CHANGE STATE
    public class AttackAndFollowState : StateBase
    {
        public readonly AttackingUnitBase unit;
        public readonly EntityBase targetEntity;

        private readonly float beginSpeed;
        public AttackAndFollowState(AttackingUnitBase unit, EntityBase targetEntity)
        {
            this.unit = unit;
            this.targetEntity = targetEntity;

            beginSpeed = unit.Agent.speed;
        }


        public override void Enter()
        {
            unit.Agent.ResetPath();


            if(!(Vector3.Distance(unit.Position, targetEntity.Position) - targetEntity.Radius < unit.CurrentWeapon.Distance / 1.5f))
            {
                ChangeStateToFollow();
            }

        }

        public override void Update()
        {



            Debug.DrawRay(unit.transform.position, targetEntity.Position - unit.Position);

            if (ShouldFollowTarget())
            {
                ChangeStateToFollow();
                return;
            }

            unit.transform.LookAt(targetEntity.Position);

            if (unit.CanShoot())
            {
                Debug.Log("Attack");
                unit.Shoot(targetEntity);
            }
        }


        public override void Exit()
        {
            unit.Agent.enabled = true;
            unit.Agent.ResetPath();

            unit.Agent.speed = beginSpeed;

        }

        private bool ShouldFollowTarget()
        {
            float distanceToTarget = Vector3.Distance(unit.Position, targetEntity.Position);
            bool isOutOfRange = distanceToTarget - targetEntity.Radius > unit.CurrentWeapon.Distance * 1.5f;

            bool hasObstacle = Physics.Raycast(
                new Ray(unit.Position, targetEntity.Position - unit.Position),
                out RaycastHit hitInfo) &&
                !hitInfo.collider.gameObject.Equals(targetEntity.gameObject);

            return isOutOfRange || hasObstacle;
        }

        private void ChangeStateToFollow()
        {
            var followState = new FollowWithChangeState(unit, targetEntity, new AttackAndFollowState(unit, targetEntity));
            //unit.StateInteractable.SetState(followState);




            unit.StateInteractable.SetState(followState);
        }
    }
}