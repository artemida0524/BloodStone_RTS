using Build;
using System.Collections.Generic;
using UnityEngine;

namespace Option
{
    public class TestOption : OptionBase
    {
        public TestOption(TestBuild build) : base(build)
        {
            Options.Add(new DoActionOption() { Action = () => Debug.Log("bemsS"), Name = "DoSomething" });
            Options.Add(new DoActionOption() { Name = "More2", Action = Do, myEnum = ActionType.More });
        }

        private void Do()
        {
            Object.FindObjectOfType<OptionSelectedGrid>().Init(GetOption());
        }

        private List<IOption> GetOption()
        {
            return new List<IOption>()
            {
                new Option()
            };
        }

        public class Option : IOption
        {
            public List<DoActionOption> Options { get; private set; }

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