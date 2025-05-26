using GlobalData;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using Game.Gameplay.Entity;
using Game.Gameplay.Build;
using Game.Gameplay.Units;

namespace Game.Gameplay.Options
{
    
    public class OptionWorkerUnit : OptionUnitBase
    {
        private TreasureBuild treasureBuild;
        private WorkerUnitBase workerUnit;

        public OptionWorkerUnit(WorkerUnitBase unit, IBuildingProvider buildingProvider) : base(unit)
        {
            this.workerUnit = unit;
            Headquarters faction = buildingProvider.GetBuilds<Headquarters>().First(a => a.FactionType == unit.FactionType);
            treasureBuild = buildingProvider.GetBuilds<TreasureBuild>().NearestEntity(unit);

            options.Add(new DoActionOption() { Action =  TestFunction, myEnum = ActionType.Once, Name = "Work1" });
        }

        private void TestFunction()
        {
            treasureBuild.Interact(new List<WorkerUnitBase>() { workerUnit });
        }

    }


    public class OptionUnitBase : OptionBase
    {
        public OptionUnitBase(UnitBase unit) : base(unit)
        {
            options.Add(new DoActionOption() { Action = unit.DoSomething, Name = nameof(unit.DoSomething), myEnum = ActionType.Once });
            options.Add(new DoActionOption() { Name = "More", Action = Do, myEnum = ActionType.More });
        }

        private void Do()
        {
            Object.FindObjectOfType<OptionSelectedGrid>().Init(GetOption());
        }

        private List<IOption> GetOption()
        {
            return new List<IOption>()
            {
                new Option(),
            };
        }

        public class Option : IOption
        {
            public IReadOnlyList<DoActionOption> Options { get; private set; }

            public Option()
            {
                Options = new List<DoActionOption>()
                {

                    new DoActionOption()
                    {
                        Name = "Optio1",
                        Action = () => Debug.Log("Optio1"),
                        myEnum = ActionType.Once
                    },

                    new DoActionOption()
                    {
                        Name = "Optio2",
                        Action = () => Debug.Log("Optio2"),
                        myEnum = ActionType.Once
                    },


                    new DoActionOption()
                    {
                        Name = "Optio3",
                        Action = () => Debug.Log("Optio3"),
                        myEnum = ActionType.Once
                    },

                };
            }
        }
    }
}