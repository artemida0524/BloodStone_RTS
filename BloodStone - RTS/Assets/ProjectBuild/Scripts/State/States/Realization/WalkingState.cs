using Unit;
using UnityEngine;

namespace State
{
    public class WalkingState : StateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 point;

        public WalkingState(UnitBase unit, Vector3 point)
        {
            this.unit = unit;
            this.point = point;
        }

        public override void Enter()
        {
            unit.Animator.Play(unit.WalkingAnimation);
            unit.Agent.SetDestination(point);
        }

        public override void Update()
        {

            //unit.Agent.SetDestination(point);



            if ((point - unit.gameObject.transform.position).sqrMagnitude < unit.Agent.stoppingDistance)
            {
                unit.Agent.ResetPath();
                IsFinished = true;
            }


        }
    }
}