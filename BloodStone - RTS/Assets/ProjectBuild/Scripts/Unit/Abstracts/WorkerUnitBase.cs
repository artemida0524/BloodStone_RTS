using Option;
using State;
using UnityEngine;
using Zenject;

namespace Unit
{
    public abstract class WorkerUnitBase : UnitBase
    {



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
    }
}