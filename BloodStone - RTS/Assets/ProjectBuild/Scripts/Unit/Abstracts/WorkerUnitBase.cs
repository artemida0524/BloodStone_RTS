using UnityEngine;
using Option;
using State;
using Interaction;
using System.Collections.Generic;
using Select;
using UnityEngine.XR;

namespace Unit
{
    [RequireComponent(typeof(AnimationEventCallBackWoker))]
    public abstract class WorkerUnitBase : UnitBase
    {
        [field :SerializeField] public AnimationEventCallBackWoker AnimationEventCallBack { get; protected set; }

        protected override void Update()
        {
            base.Update();

            //Debug.Log(StateInteractable.Behaviour.StateMachine.State + " " + name);
        }

        
        public override bool MoveTo(Vector3 point, float radius)
        {
            if (CanMove)
            {
                StateInteractable.MoveState.ChangeState(new RunningState(this, point, radius));
            }
            return CanMove;
        }


        public override IOption InitOption()
        {
            return new OptionWorkerUnit(this);
        }

        protected override StateBehaviourBase InitializeState()
        {
            return new WorkerStateBehaviour(this);
        }
    }
}