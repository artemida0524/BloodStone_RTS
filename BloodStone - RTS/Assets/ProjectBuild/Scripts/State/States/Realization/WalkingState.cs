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

            
        }

        public override void Update()
        {

            unit.Agent.SetDestination(point);
            if ((point - unit.gameObject.transform.position).sqrMagnitude < 0.05f)
            {
                unit.Agent.ResetPath();
                IsFinished = true;
            }
        }
    }
}