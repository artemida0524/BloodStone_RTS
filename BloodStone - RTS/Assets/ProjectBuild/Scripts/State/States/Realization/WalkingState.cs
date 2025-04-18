using Cinemachine;
using Entity;
using System.Collections;
using System.Drawing;
using Unit;
using UnityEngine;

namespace State
{
    public class WalkingState : MovableStateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 point;
        private readonly float radius;

        public WalkingState(UnitBase unit, Vector3 point, float radius)
        {
            this.unit = unit;
            this.point = point;
            this.radius = radius;
        }

        public override void Enter()
        {
            unit.Animator.Play(unit.WalkingAnimation);
            SetDestinationAsyncRunner(unit, point);
        }

        public override void Update()
        {

            //Debug.Log((point - unit.Position).magnitude + ": " + (unit.Agent.stoppingDistance + radius));

            if ((point - unit.Position).magnitude < unit.Agent.stoppingDistance + radius)
            {
                unit.Agent.ResetPath();
                IsFinished = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
            //unit.Agent.ResetPath();
        }

    }
}