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
        private readonly bool automaticIdleAnimation;

        public WalkingState(UnitBase unit, Vector3 point, float radius, bool automaticIdleAnimation = true)
        {
            this.unit = unit;
            this.point = point;
            this.radius = radius;
            this.automaticIdleAnimation = automaticIdleAnimation;
        }

        public override void Enter()
        {
            unit.Animator.Play(unit.WalkingAnimation);
            SetDestinationAsyncRunner(unit, point);
        }

        public override void Update()
        {
            if ((point - unit.Position).magnitude < unit.Agent.stoppingDistance + radius)
            {
                unit.Agent.ResetPath();
                if (automaticIdleAnimation)
                {
                    unit.Animator.Play(unit.IdleAnimation);
                }
                IsFinished = true;
            }
        }
    }
}