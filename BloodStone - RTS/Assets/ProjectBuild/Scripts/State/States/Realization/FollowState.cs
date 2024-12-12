using Entity;
using System.Collections;
using System.Threading.Tasks;
using Unit;
using UnityEngine;

//using UnitBase = Unit.UnitBase;

namespace State
{


    public abstract class MovableStateBase : StateBase
    {
        protected bool isSetDirection = true;

        protected async void SetDirectionAsyncRunner(UnitBase unit, EntityBase target, int interval = 100)
        {
            await SetDirectionAsync(unit, target, interval);
        }

        private async Task SetDirectionAsync(UnitBase unit, EntityBase target, int interval = 100)
        {
            unit.Agent.SetDestination(target.transform.position);
            while (isSetDirection)
            {
                unit.Agent.SetDestination(target.transform.position);
                await Task.Delay(interval);

            }
        }

        public override void Exit()
        {
            isSetDirection = false;
        }


    }

    public class FollowState : MovableStateBase
    {
        private readonly AttackingUnitBase unit;
        private readonly EntityBase targetEntity;
        private readonly StateBase changedState;

        public FollowState(AttackingUnitBase unit, EntityBase targetEntity, StateBase changedState)
        {
            this.unit = unit;
            this.targetEntity = targetEntity;
            this.changedState = changedState;
        }

        public override void Enter()
        {
            SetDirectionAsyncRunner(unit, targetEntity, 500);

            unit.Animator.Play(unit.RunningAnimation);

            unit.Agent.speed = 6f;
        }

        public override void Update()
        {


            if (Physics.Raycast(new Ray(unit.transform.position, targetEntity.transform.position - unit.transform.position), out RaycastHit hitInfo1))
            {
                Debug.Log(hitInfo1.collider.name);
            }

            Debug.DrawRay(unit.transform.position, targetEntity.transform.position - unit.transform.position);

            if (Vector3.Distance(unit.transform.position, targetEntity.transform.position) < unit.Weapon.Distance / 1.5f)
            {
                if (Physics.Raycast(new Ray(unit.transform.position, targetEntity.transform.position - unit.transform.position), out RaycastHit hitInfo))
                {
                    if(hitInfo.collider.gameObject.Equals(targetEntity.gameObject))
                    {
                        unit.StateInteractable.SetState(changedState);
                    }
                }
            }
        }

        public override void Exit()
        {
            base.Exit();

            unit.Agent.ResetPath();

        }
    }
}




