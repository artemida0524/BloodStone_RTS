using Build;
using Currency;
using GlobalData;
using State;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using Entity;

namespace Option
{

    public class OptionWorkerUnit : OptionUnitBase
    {
        private TreasureBuild treasureBuild;
        private WorkerUnitBase workerUnit;
        public OptionWorkerUnit(WorkerUnitBase unit) : base(unit)
        {
            this.workerUnit = unit;
            Build.Faction faction = GlobalBuildsDataHandler.GetBuilds<Build.Faction>().First(a => a.FactionType == unit.FactionType);
            treasureBuild = GlobalBuildsDataHandler.GetBuilds<TreasureBuild>().NearestEntity(unit);

            options.Add(new DoActionOption() { Action =  AAAa, myEnum = ActionType.Once, Name = "Work1" });
        }

        private void AAAa()
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