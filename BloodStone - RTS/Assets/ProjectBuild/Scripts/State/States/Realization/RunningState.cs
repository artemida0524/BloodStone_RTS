using Unit;
using UnityEngine;

namespace State
{
    public class RunningState : StateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 point;

        private readonly float beginSpeed;
        public RunningState(UnitBase unit, Vector3 point)
        {
            this.unit = unit;
            this.point = point;

            this.beginSpeed = unit.Agent.speed;
        }


        public override void Enter()
        {
            unit.Agent.SetDestination(point);
            unit.Animator.Play(AnimationStateNames.RUNNING);
            unit.Agent.speed = 6f;
        }


        public override void Update()
        {
            //unit.Agent.SetDestination(point);
            Debug.Log(unit.Agent.pathStatus);

            if ((point - unit.gameObject.transform.position).sqrMagnitude < unit.Agent.stoppingDistance)
            {
                unit.Agent.ResetPath();
                IsFinished = true;
            }
        }


        public override void Exit()
        {
            unit.Agent.speed = beginSpeed;
        }

    }
}