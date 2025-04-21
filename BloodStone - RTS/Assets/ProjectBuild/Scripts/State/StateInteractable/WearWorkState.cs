using Currency;
using Unit;
using UnityEngine;

namespace State
{

    // MUST BE CHANGED
    public class WearWorkState : StateBase
    {
        private readonly WearWorkerUnit unit;
        private readonly ICurrencyStorage fromStorage;
        private readonly ICurrencyStorage toStorage;
        private readonly ICurrency currentCurrencyType;

        private int amount;

        private bool takeCurrency = false;

        public WearWorkState(WearWorkerUnit unit, ICurrencyStorage fromStorage, ICurrencyStorage toStorage, ICurrency currentCurrency)
        {
            this.unit = unit; 
            this.fromStorage = fromStorage;
            this.toStorage = toStorage;
            this.currentCurrencyType = currentCurrency;
        }

        public override void Enter()
        {
            unit.MoveTo(fromStorage.Position, fromStorage.Radius);
        }

        public override void Update()
        {
            if(fromStorage.GetFirstCurrency().Count < 30)
            {
                IsFinished = true;
            }

            if(unit.StateInteractable.MoveState.State.IsFinished  && !takeCurrency)
            {
                if (fromStorage.GetCurrencyByType(currentCurrencyType).Spend(30))
                {
                    amount = 30;
                    unit.MoveTo(toStorage.Position, toStorage.Radius);

                    takeCurrency = true;
                }
            }
            else if(unit.StateInteractable.MoveState.State.IsFinished && takeCurrency)
            {
                toStorage.AddCurrencyByType(currentCurrencyType, amount);
                amount = 0;
                unit.MoveTo(fromStorage.Position, fromStorage.Radius);

                takeCurrency = false;
            }
        }
    }

}