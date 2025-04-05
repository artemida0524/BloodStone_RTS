using System;
using Unit;
using UnityEngine;

namespace State
{
    public class MoveStateWithActionIfFinished : StateBase
    {
        private readonly UnitBase unit;
        private readonly Vector3 target;
        private readonly float radius;
        private readonly Action<UnitBase> action;

        public MoveStateWithActionIfFinished(UnitBase unit, Vector3 target, float radius, Action<UnitBase> action)
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
                //IsFinished = true;
            }
        }

        public override void Exit()
        {
            unit.ResetMove();
        }
    }

}