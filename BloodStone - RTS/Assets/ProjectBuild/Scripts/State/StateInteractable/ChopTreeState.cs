using Game.Gameplay.Build;
using Game.Gameplay.Entity;
using Game.Gameplay.Units;
using Currency;
using GlobalData;
using UnityEngine;

namespace State
{
    public class ChopTreeState : StateBase
    {
        private readonly ChopTreeWorkerUnit unit;
        private readonly ICurrencyStorage storage;
        private TreeBuildBase tree;
        private IBuildingProvider _buildingProvider;

        private StateMachine machine = new StateMachine(null);

        public ChopTreeState(ChopTreeWorkerUnit unit, TreeBuildBase tree, ICurrencyStorage storage, IBuildingProvider buildingProvider)
        {
            this.unit = unit;
            this.tree = tree;
            this.storage = storage;
            _buildingProvider = buildingProvider;
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

                        case GoToStorageFinish:

                            IsFinished = true;

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

            TreeBuildBase newTree = _buildingProvider.GetBuilds<TreeBuildBase>(tree => !tree.Equals(this.tree) && !tree.IsDone).NearestEntity(tree);

            if (newTree == null)
            {
                if(unit.TreeCurrency.Count > 0)
                {
                    machine.ChangeState(new GoToStorageFinish(unit, storage));
                }
                else
                {
                    IsFinished = true;
                    machine.ChangeState(null);
                }
                return;
            }

            tree = newTree;

            switch (machine.State)
            {
                case GoToTree:
                    machine.ChangeState(new GoToTree(unit, tree));
                    break;

                case Work:
                    machine.ChangeState(new GoToTree(unit, tree));
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

            private bool chop = false;

            public Work(ChopTreeWorkerUnit unit, TreeBuildBase currentTree)
            {
                this.unit = unit;
                this.tree = currentTree;
            }

            public override void Enter()
            {
                unit.AnimationEventCallBack.OnChopTree += OnCallHandler;
                unit.Animator.Play(AnimationStateNames.CHOP_TREE);
                Debug.Log("Enter");
            }

            private void OnCallHandler()
            {
                chop = true;
            }

            public override void Update()
            {

                if (chop)
                {
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
                unit.AnimationEventCallBack.OnChopTree -= OnCallHandler;
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
                unit.MoveTo(storage.Position, storage.Radius + 1);
            }

            public override void Update()
            {
                if (unit.StateInteractable.MoveState.State.IsFinished)
                {
                    storage.AddCurrencyByName(unit.TreeCurrency.Name, unit.TreeCurrency.SpendAll());
                    IsFinished = true;
                }
            }
        }


        private class GoToStorageFinish : StateBase
        {
            private ChopTreeWorkerUnit unit;
            private ICurrencyStorage storage;

            public GoToStorageFinish(ChopTreeWorkerUnit unit, ICurrencyStorage storage)
            {
                this.unit = unit;
                this.storage = storage;
            }

            public override void Enter()
            {
                unit.MoveTo(storage.Position, storage.Radius + 1);
            }

            public override void Update()
            {
                if (unit.StateInteractable.MoveState.State.IsFinished)
                {
                    storage.AddCurrencyByName(ResourceNames.TREE, unit.TreeCurrency.SpendAll());
                    IsFinished = true;
                }
            }
        }

    }
}