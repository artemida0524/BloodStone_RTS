using Currency;
using Unit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
            unit.Agent.SetDestination(fromStorage.Position);
            unit.Animator.Play(unit.WalkingAnimation);
        }


        public override void Update()
        {
            if(Vector3.Distance(unit.Position, fromStorage.Position) < 5f && !takeCurrency)
            {
                if (fromStorage.GetCurrency(currentCurrencyType).Spend(30))
                {
                    amount = 30;
                    unit.Agent.SetDestination(toStorage.Position);
                    takeCurrency = true;
                }
            }
            else if(Vector3.Distance(unit.Position, toStorage.Position) < 5f && takeCurrency)
            {
                toStorage.AddCurrencyByType(currentCurrencyType, amount);
                amount = 0;
                unit.Agent.SetDestination(fromStorage.Position);

                takeCurrency = false;
            }
        }

        public override void Exit()
        {
            unit.Agent.ResetPath();
        }

    }
}