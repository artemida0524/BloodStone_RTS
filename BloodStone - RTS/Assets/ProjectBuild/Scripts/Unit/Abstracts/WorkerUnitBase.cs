using UnityEngine;
using Option;
using State;
using Interaction;
using System.Collections.Generic;
using Select;
using UnityEngine.XR;
using Zenject;
using GlobalData;

namespace Unit
{
    [RequireComponent(typeof(AnimationEventCallBackWoker))]
    public abstract class WorkerUnitBase : UnitBase
    {
        [field: SerializeField] public AnimationEventCallBackWoker AnimationEventCallBack { get; protected set; }


        private GlobalBuildsDataHandler allBuildData;


        [Inject]
        private void Construct(GlobalBuildsDataHandler AllBuild)
        {
            allBuildData = AllBuild;
        }

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
            return new OptionWorkerUnit(this, allBuildData);
        }

        protected override StateBehaviourBase InitializeState()
        {
            return new WorkerStateBehaviour(this);
        }
    }
}