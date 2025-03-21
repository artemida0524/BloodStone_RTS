using Build;
using Entity;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Option
{

    public class OptionUnitBase : OptionBase
    {
        public OptionUnitBase(UnitBase unitbase) : base(unitbase)
        {
            Options.Add(new DoActionOption() { Action = unitbase.DoSomething, Name = nameof(unitbase.DoSomething) });
        }
    }

    public class TestOption : OptionBase
    {
        private TestBuild testBuild;
        public TestOption(TestBuild build) : base(build)
        {
            this.testBuild = build;
            OptionSelectedGrid optionSelectedGrid = Object.FindObjectOfType<OptionSelectedGrid>();

            Options.Add(new DoActionOption() { Action = () => optionSelectedGrid.Init(GetOptions()), Name = "Beeem" });
            Options.Add(new DoActionOption() { Action = () => Debug.Log("bemsS"), Name = "DoSomething" });
        }

        private List<IOption> GetOptions()
        {

            List<IOption> interactables = new List<IOption>()
            {
                new Interact(testBuild),
            };

            return interactables;
        }


        private class Interact : IOption
        {
            public EntityInfoSO EntityInfo { get; private set; }
            public List<DoActionOption> Options { get; private set; }



            public Interact(EntityBase entity)
            {

                this.EntityInfo = entity.EntityInfo;

                Options = new List<DoActionOption>()
                    {
                        new DoActionOption() { Name = "Optio1", Action = () => Debug.Log("Action1")},
                        new DoActionOption() { Name = "Optio2", Action = () => Debug.Log("Action2")},
                        new DoActionOption() { Name = "Optio3", Action = () => Debug.Log("Action3")}
                    };
            }

        }
    }
}