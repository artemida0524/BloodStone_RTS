using Game.Gameplay.Selection;
using Game.Gameplay.Units;
using Game.Gameplay.Units.Utils;
using State;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Build
{
    public class SwitcherBuild : BuildInteractableBase
    {
        [SerializeField] private Transform entryPoint;
        [SerializeField] private Transform exitPoint;

        private Dictionary<UnitBase, DataUnit> dataUnits = new Dictionary<UnitBase, DataUnit>();

        protected override void OnEnable()
        {
            base.OnEnable();
            UnitUtility.OnUnitDisableOrDestroy += RemoveUnitHandler;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnitUtility.OnUnitDisableOrDestroy -= RemoveUnitHandler;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnitUtility.OnUnitDisableOrDestroy -= RemoveUnitHandler;
        }

        public override void Interact()
        {
            throw new System.NotImplementedException();
        }

        public override void Interact(IReadOnlyList<ISelectable> units)
        {
            throw new System.NotImplementedException();
        }

        public void SwitchUnits(List<UnitBase> units, UnitBase type, Action<UnitBase> actionIfFinishedSwitched)
        {
            foreach (var unit in units)
            {
                SwitchUnit(unit, type, actionIfFinishedSwitched);
            }
        }

        private void SwitchUnit(UnitBase currentUnit, UnitBase type, Action<UnitBase> actionIfFinishedSwitched)
        {
            currentUnit.SetState(new MoveStateWithActionIfFinished<UnitBase>(currentUnit, entryPoint.position, 0.5f, UnitWasFinished));

            UnitTask task = new UnitTask(type, actionIfFinishedSwitched);
            StateChangeChecker checker = new StateChangeChecker(currentUnit, Detect);

            DataUnit dataUnit = new DataUnit(currentUnit, task, checker);
            dataUnits[currentUnit] = dataUnit;
        }

        private void Detect(UnitBase unit, StateBase state)
        {
            DataUnit data = dataUnits[unit];

            data.Dispose();
            dataUnits.Remove(data.Unit);
        }

        private void RemoveUnit(DataUnit data)
        {
            data.Dispose();
            dataUnits.Remove(data.Unit);
            Destroy(data.Unit.gameObject);
        }

        private void RemoveUnitHandler(UnitBase unit)
        {
            if (dataUnits.ContainsKey(unit))
            {
                RemoveUnit(dataUnits[unit]);
            }
        }

        private void UnitWasFinished(UnitBase unit)
        {
            DataUnit data = dataUnits[unit];
            UnitBase newUnit = Instantiate(data.Task.Type, exitPoint.position, Quaternion.identity);

            data.Task.ActionIfFinishedSwitched(newUnit);

            RemoveUnit(data);
        }

        private class DataUnit : IDisposable
        {
            public UnitBase Unit { get; private set; }
            public UnitTask Task { get; private set; }
            public StateChangeChecker Checker { get; private set; }   

            public DataUnit(UnitBase unit, UnitTask unitTask, StateChangeChecker checker)
            {
                this.Unit = unit;
                this.Task = unitTask;
                this.Checker = checker;
            }

            public void Dispose()
            {
                Checker.Dispose();
            }
        }

        private class UnitTask
        {
            public UnitBase Type { get; private set; }
            public Action<UnitBase> ActionIfFinishedSwitched { get; private set; }
            public UnitTask(UnitBase type, Action<UnitBase> actionIfFinishedSwitched)
            {
                this.Type = type;
                this.ActionIfFinishedSwitched = actionIfFinishedSwitched;
            }
        }

        private class StateChangeChecker : IDisposable
        {
            public UnitBase Unit { get; private set; }
            private Action<UnitBase, StateBase> actionStateChange;

            public StateChangeChecker(UnitBase unit, Action<UnitBase, StateBase> actionStateChange)
            {
                this.Unit = unit;
                this.actionStateChange = actionStateChange;
                Unit.OnStateChanged += OnStateChangeHandler;
            }

            private void OnStateChangeHandler(StateBase state)
            {
                actionStateChange(Unit, state);
            }

            public void Dispose()
            {
                Unit.OnStateChanged -= OnStateChangeHandler;
            }
        }
    }
}