using Build;
using Currency;
using Entity;
using GlobalData;
using System.Collections;
using Unit;
using UnityEngine;

namespace State
{
    public class ChopTreeState : StateBase
    {
        private readonly ChopTreeWorkerUnit unit;
        private readonly ICurrencyStorage storage;
        private TreeBuildBase tree;

        private StateMachine machine = new StateMachine(null);

        public ChopTreeState(ChopTreeWorkerUnit unit, TreeBuildBase tree, ICurrencyStorage storage)
        {
            this.unit = unit;
            this.tree = tree;
            this.storage = storage;
        }

        public override void Enter()
        {
            machine.ChangeState(new GoToTree(unit, tree));

            tree.OnTreeOver += OnTreeOverHandler;
        }


        public override void Update()
        {
            if (IsFinished) return;

            machine.Update();

            try
            {
                if (machine.State.IsFinished)
                {
                    switch (machine.State)
                    {
                        case GoToTree:
                            machine.ChangeState(new Work(unit, tree));
                            break;

                        case Work:
                            machine.ChangeState(new GoToStorage(unit, storage));
                            break;

                        case GoToStorage:
                            machine.ChangeState(new GoToTree(unit, tree));
                            break;
                    }
                }
            }
            catch (System.Exception)
            {
                Debug.Log(machine.State + " " + IsFinished);
            }
        }

        private void OnTreeOverHandler()
        {
            tree.OnTreeOver -= OnTreeOverHandler;

            TreeBuildBase newTree = GlobalBuildsDataHandler.GetBuilds<TreeBuildBase>(tree => !tree.Equals(this.tree) && !tree.IsDone).NearestEntity(tree);


            if(newTree == null)
            {
                IsFinished = true;
                machine.ChangeState(null);
                return;
            }

            tree = newTree;

            switch (machine.State)
            {
                case GoToTree:

                    if (tree is null)
                    {
                        IsFinished = true;
                    }
                    else
                    {
                        machine.ChangeState(new GoToTree(unit, tree));
                    }
                    break;

                case Work work:

                    if (tree is null)
                    {
                        machine.ChangeState(new GoToStorage(unit, storage));
                    }
                    else
                    {
                        machine.ChangeState(new GoToTree(unit, tree)); 
                    }

                    break;
            }

            if (tree is not null)
            {
                tree.OnTreeOver += OnTreeOverHandler; 
            }
        }

        private class GoToTree : StateBase
        {
            private UnitBase unit;
            public TreeBuildBase CurrentTree { get; private set; }
            public GoToTree(UnitBase unit, TreeBuildBase tree)
            {
                this.unit = unit;
                this.CurrentTree = tree;
            }

            public override void Enter()
            {
                unit.MoveTo(CurrentTree.Position, CurrentTree.Radius + 3);
            }

            public override void Update()
            {
                if (unit.StateInteractable.MoveState.State.IsFinished)
                {
                    IsFinished = true;
                }
            }
        }

        private class Work : StateBase
        {
            private ChopTreeWorkerUnit unit;
            private TreeBuildBase tree;

            private const float TIME_COOL_DOWN = 1f;
            private float time = 0f;

            private bool chop = false;

            public Work(ChopTreeWorkerUnit unit, TreeBuildBase currentTree)
            {
                this.unit = unit;
                this.tree = currentTree;
            }

            public override void Enter()
            {
                unit.OnCall += Unit_OnCall;
                unit.Animator.Play("Chop");
                Debug.Log("Enter");
            }

            private void Unit_OnCall()
            {
                chop = true;
            }

            public override void Update()
            {
                //time += Time.deltaTime;

                //if(time > TIME_COOL_DOWN)
                //{

                //    if (tree is null)
                //    {
                //        IsFinished = true;
                //        return;
                //    }

                //    //try
                //    //{
                //    if (tree.TreeCurrency.Spend(5))
                //    {
                //        unit.TreeCurrency.Add(5);
                //    }
                //    else
                //    {
                //        int amount = tree.TreeCurrency.SpendAll();

                //        unit.TreeCurrency.Add(amount);
                //        IsFinished = true;
                //        return;

                //    }

                //    if (unit.TreeCurrency.IsFull)
                //    {
                //        IsFinished = true;
                //    }
                //    //}
                //    //catch (System.Exception)
                //    //{

                //    //    IsFinished = true;
                //    //}

                //    time = 0f;
                //}

                if (chop)
                {

                    if (tree is null)
                    {
                        IsFinished = true;
                        return;
                    }

                    if (tree.TreeCurrency.Spend(5))
                    {
                        unit.TreeCurrency.Add(5);
                    }
                    else
                    {
                        int amount = tree.TreeCurrency.SpendAll();

                        unit.TreeCurrency.Add(amount);
                        IsFinished = true;
                        return;

                    }

                    if (unit.TreeCurrency.IsFull)
                    {
                        IsFinished = true;
                    }

                    chop = false;
                }

                
            }
            public override void Exit()
            {
                unit.OnCall -= Unit_OnCall;
            }
        }

        private class GoToStorage : StateBase
        {
            private ChopTreeWorkerUnit unit;
            private ICurrencyStorage storage;

            public GoToStorage(ChopTreeWorkerUnit unit, ICurrencyStorage storage)
            {
                this.unit = unit;
                this.storage = storage;
            }

            public override void Enter()
            {
                //if (storage == null)
                //{
                //    IsFinished = true;
                //}
                unit.MoveTo(storage.Position, storage.Radius + 1);
            }

            public override void Update()
            {
                if (unit.StateInteractable.MoveState.State.IsFinished)
                {
                    storage.AddCurrencyByType(unit.TreeCurrency, unit.TreeCurrency.SpendAll());
                    IsFinished = true;
                }
            }
        }
    }
}