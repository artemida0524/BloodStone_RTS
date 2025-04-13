using Currency;
using UnityEngine;

namespace Unit
{
    public class ChopTreeWorkerUnit : WorkerUnitBase
    {
        public ICurrency TreeCurrency { get; private set; } = new TreeCurrency(0, 30);

        protected override void Update()
        {
            base.Update();

            //Debug.Log(StateInteractable.Behaviour.StateMachine.State);
        }
    }

}