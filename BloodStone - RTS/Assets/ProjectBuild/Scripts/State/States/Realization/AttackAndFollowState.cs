//using Entity;
//using Unit;
//using UnityEngine;

//namespace State
//{
//    public class AttackAndFollowState : StateBase
//    {
//        private readonly AttackingUnitBase unit;
//        private readonly EntityBase targetEntity;
//        private readonly float initialSpeed;

//        public AttackAndFollowState(AttackingUnitBase unit, EntityBase targetEntity)
//        {
//            this.unit = unit;
//            this.targetEntity = targetEntity;
//            initialSpeed = unit.Agent.speed;
//        }

//        public override void Enter()
//        {
//            unit.Agent.ResetPath();
//            unit.Animator.Play(unit.IdleAnimation);
//            Debug.Log("Enter AttackAndFollowState");
//        }

//        public override void Update()
//        {
//            var direction = targetEntity.transform.position - unit.transform.position;
//            Debug.DrawRay(unit.transform.position, direction);

//            if (unit.Agent.pathPending || unit.Agent.remainingDistance > unit.Weapon.Distance * 1.5f)
//            {
//                TransitionToFollowState();
//                return;
//            }

//            if (Physics.Raycast(unit.transform.position, direction, out RaycastHit hitInfo))
//            {
//                if (!hitInfo.collider.gameObject.Equals(targetEntity.gameObject))
//                {
//                    TransitionToFollowState();
//                    return;
//                }
//            }

//            unit.transform.LookAt(targetEntity.transform.position);

//            if (unit.CanShoot())
//            {
//                unit.Shoot(targetEntity);
//            }
//        }

//        public override void Exit()
//        {
//            unit.Agent.ResetPath();
//            unit.Agent.speed = initialSpeed;
//        }

//        private void TransitionToFollowState()
//        {
//            unit.StateInteractable.SetState(new FollowState(unit, targetEntity, this));
//        }
//    }
//}


using Entity;
using Unit;
using UnityEngine;

namespace State
{

    public class AttackAndFollowState : StateBase
    {
        private readonly AttackingUnitBase unit;
        private readonly EntityBase targetEntity;

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
            unit.Animator.Play(unit.IdleAnimation);
        }

        public override void Update()
        {

            Debug.DrawRay(unit.transform.position, targetEntity.transform.position - unit.transform.position);

            if (Vector3.Distance(unit.transform.position, targetEntity.transform.position) > unit.Weapon.Distance * 1.5f)
            {
                unit.StateInteractable.SetState(new FollowState(unit, targetEntity, new AttackAndFollowState(unit, targetEntity)));
                return;
            }
            else if (Physics.Raycast(new Ray(unit.transform.position, targetEntity.transform.position - unit.transform.position), out RaycastHit hitInfo1) && !hitInfo1.collider.gameObject.Equals(targetEntity.gameObject))
            {
                unit.StateInteractable.SetState(new FollowState(unit, targetEntity, new AttackAndFollowState(unit, targetEntity)));
                return;
            }
            else
            {

                unit.transform.LookAt(targetEntity.transform.position);
                if (unit.CanShoot())
                {
                    unit.Shoot(targetEntity);
                }
            }
        }

        public override void Exit()
        {
            unit.Agent.enabled = true;
            unit.Agent.ResetPath();

            unit.Agent.speed = beginSpeed;

        }
    }
}