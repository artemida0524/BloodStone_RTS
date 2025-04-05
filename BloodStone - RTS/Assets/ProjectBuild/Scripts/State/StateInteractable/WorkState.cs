using Currency;
using Unit;
using UnityEngine;

namespace State
{
    public class WorkState : StateBase
    {
        private readonly WorkerUnitBase unit;
        private readonly ICurrencyStorage fromStorage;
        private readonly ICurrencyStorage toStorage;
        private readonly ICurrency currentCurrencyType;

        private int amount;

        private bool takeCurrency = false;

        public WorkState(WorkerUnitBase unit, ICurrencyStorage fromStorage, ICurrencyStorage toStorage, ICurrency currentCurrency)
        {
            this.unit = unit; 
            this.fromStorage = fromStorage;
            this.currentCurrencyType = currentCurrency;
            this.toStorage = toStorage;
        }

        public override void Enter()
        {
            unit.MoveTo(fromStorage.Position, fromStorage.Radius);
        }

        public override void Update()
        {
            if(Vector3.Distance(unit.Position, fromStorage.Position) < 5f && !takeCurrency)
            {
                if (fromStorage.GetCurrency(currentCurrencyType).Spend(30))
                {
                    amount = 30;
                    unit.MoveTo(toStorage.Position, toStorage.Radius);

                    takeCurrency = true;
                }
            }
            else if(Vector3.Distance(unit.Position, toStorage.Position) < 5f && takeCurrency)
            {
                toStorage.AddCurrencyByType(currentCurrencyType, amount);
                amount = 0;
                unit.MoveTo(fromStorage.Position, fromStorage.Radius);

                takeCurrency = false;
            }
        }
    }
}