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
        private readonly ICurrencyStorage treasureStorage;
        private readonly Build.Faction faction;
        private readonly ICurrency currentCurrency;

        private int amount;

        private bool takeCurrency = false;

        public WorkState(WorkerUnitBase unit, ICurrencyStorage treasureStorage, Build.Faction currentFaction, ICurrency currentCurrency)
        {
            this.unit = unit; 
            this.treasureStorage = treasureStorage;
            this.currentCurrency = currentCurrency;
            this.faction = currentFaction;
        }

        public override void Enter()
        {
           
            
            unit.Agent.SetDestination(treasureStorage.Position);
            unit.Animator.Play(unit.WalkingAnimation);
        }


        public override void Update()
        {
            if(Vector3.Distance(unit.Position, treasureStorage.Position) < 10f && !takeCurrency)
            {
                if (treasureStorage.GetFirstCurrency().Spend(30))
                {
                    amount = 30;
                    unit.Agent.SetDestination(faction.Position);
                    takeCurrency = true;
                }
            }
            else if(Vector3.Distance(unit.Position, faction.Position) < 10f && takeCurrency)
            {
                faction.AddCurrency<Gold>(amount);
                amount = 0;
                unit.Agent.SetDestination(treasureStorage.Position);

                takeCurrency = false;
            }
            Debug.Log("Update");
        }

        public override void Exit()
        {

            unit.Agent.ResetPath();

        }

    }
}