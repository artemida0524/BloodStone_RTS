using System.Collections.Generic;
using System.Linq;
using Unit;
using Select;
using Game.Gameplay.Build;
using Game.Gameplay.Selection;
using Game.Gameplay.Units;
using System;

namespace State
{
    public class NotBuildState : StateBase
    {
        private readonly BuildInteractableBase _build;
        public NotBuildState(BuildInteractableBase build)
        {
            _build = build;
        }

        public override void Enter()
        {
            base.Enter();
            _build.CanInteraction = false;
            _build.OnInteractWithSelectables += OnInteractWithSelectablesHandler;
            _build.Health.OnDataChange += OnHealthChangeHandler;
        }

        public override void Exit()
        {
            base.Exit();
            _build.CanInteraction = true;
            _build.OnInteractWithSelectables -= OnInteractWithSelectablesHandler;
            _build.Health.OnDataChange -= OnHealthChangeHandler;
        }

        private void OnInteractWithSelectablesHandler(IReadOnlyList<ISelectable> selectables)
        {
            List<BuilderWorkerUnit> units = selectables.OfType<BuilderWorkerUnit>().ToList();
            units.ForEach(unit => unit.SetState(new MoveStateWithActionIfFinished<BuilderWorkerUnit>(unit, _build.Position, _build.Radius, OnUnitFinished)));
        }

        private void OnUnitFinished(BuilderWorkerUnit unit)
        {
            unit.SetState(new BuildWorkerState(unit, _build));
        }

        private void OnHealthChangeHandler(object sender, EventArgs e)
        {
            if(_build.Health.IsMaxHealth)
            {
                IsFinished = true;
            }
        }
    }
}