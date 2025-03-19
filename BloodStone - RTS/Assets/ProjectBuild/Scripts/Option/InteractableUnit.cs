using Build;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Option
{

    public class InteractableUnit : InteractableBase
    {
        public InteractableUnit(UnitBase unitbase) : base(unitbase)
        {
            Actions.Add(new DoActionOption() { Action = unitbase.DoSomething, Name = nameof(unitbase.DoSomething) });
        }
    }


    public class TestInteractable : InteractableBase
    {
        public TestInteractable(TestBuild build) : base(build)
        {
            OptionSelectedGrid optionSelectedGrid = GameObject.FindObjectOfType<OptionSelectedGrid>();

            Actions.Add(new DoActionOption() { Action = () => optionSelectedGrid.Init(GetInteractables()), Name = "Beeem" });
        }

        private List<IInteractable> GetInteractables()
        {

            List<IInteractable> interactables = new List<IInteractable>()
            {
                new Interact()
            };

            return interactables;
        }





        private class Interact : IInteractable
        {
            public Sprite Icon { get; }


            public List<DoActionOption> Actions
            {

                get
                {
                    return new List<DoActionOption>()
                    {
                        new DoActionOption() { Name = "Optio1", Action = () => Debug.Log("Action1")},
                        new DoActionOption() { Name = "Optio2", Action = () => Debug.Log("Action2")},
                        new DoActionOption() { Name = "Optio3", Action = () => Debug.Log("Action3")}
                    };
                }
                private set
                {

                }
            }

        }


    }

}
