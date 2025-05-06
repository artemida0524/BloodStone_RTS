using System;
using Unit;
using UnityEngine;

namespace State
{
    public class MoveStateWithActionIfFinished<T> : StateBase where T: UnitBase
    {
        private readonly T unit;
        private readonly Vector3 target;
        private readonly float radius;
        private readonly Action<T> action;

        public MoveStateWithActionIfFinished(T unit, Vector3 target, float radius, Action<T> action)
        {
            this.unit = unit;
            this.target = target;
            this.radius = radius;
            this.action = action;
        }

        public override void Enter()
        {
            unit.MoveTo(target, radius);
        }

        public override void Update()
        {
            if (unit.StateInteractable.MoveState.State.IsFinished)
            {
                action(unit);
            }
        }

        public override void Exit()
        {
            unit.ResetMove();
        }
    }

}