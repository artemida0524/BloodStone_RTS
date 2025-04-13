using Option;
using State;
using System;
using UnityEngine;
using Zenject;

namespace Unit
{
    public abstract class WorkerUnitBase : UnitBase
    {

        public override bool MoveTo(Vector3 point, float radius)
        {
            if (CanMove)
            {
                StateInteractable.MoveState.ChangeState(new RunningState(this, point, radius));
            }
            return CanMove;
        }

        //public override bool MoveTo(Vector3 point, float radius, bool automaticIdleAnimation)
        //{
        //    if (CanMove)
        //    {
        //        StateInteractable.MoveState.ChangeState(new RunningState(this, point, radius, automaticIdleAnimation));
        //    }
        //    return CanMove;
        //}

        protected override void Update()
        {
            base.Update();

            //Debug.Log(StateInteractable.Behaviour.StateMachine.State + " " + name);

        }

        public override IOption InitOption()
        {
            return new OptionWorkerUnit(this);
        }

        protected override StateBehaviourBase InitializeState()
        {
            return new WorkerStateBehaviour(this);
        }


        public event Action OnCall;
        private void CallBack()
        {
            OnCall?.Invoke();
        }

    }
}