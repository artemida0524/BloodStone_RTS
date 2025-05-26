using Game.Gameplay.Build;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Options
{
    public class TestOption : OptionBase
    {
        public TestOption(TestBuild build) : base(build)
        {
            options.Add(new DoActionOption() { Action = () => Debug.Log("bemsS"), Name = "DoSomething" });
            options.Add(new DoActionOption() { Name = "More2", Action = Do, myEnum = ActionType.More });
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