using State;
using System.Collections;
using System.Collections.Generic;
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
            unit.Animator.Play(AnimationStateNames.WALKING);

            Debug.Log("Enter Walking state");
        }

        public override void Update()
        {
            Debug.Log("Update Walking state");

            unit.Agent.SetDestination(point);
            if ((point - unit.gameObject.transform.position).sqrMagnitude < 0.05f)
            {
                IsFinished = true;
            }
        }
    }
}