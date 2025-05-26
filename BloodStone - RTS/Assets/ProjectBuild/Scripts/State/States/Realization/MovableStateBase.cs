using Game.Gameplay.Entity;
using Game.Gameplay.Units;
using System.Threading.Tasks;
using UnityEngine;

namespace State
{
    // CHANGE STATE
    public abstract class MovableStateBase : StateBase
    {
        protected bool isSetDestination = true;

        protected async void SetDestinationAsyncRunner(UnitBase unit, IEntity target, int interval = 100)
        {
            await SetDestinationAsync(unit, target, interval);
        }

        protected async void SetDestinationAsyncRunner(UnitBase unit, Vector3 point, int interval = 100)
        {
            await SetDestinationAsync(unit, point, interval);
        }

        private async Task SetDestinationAsync(UnitBase unit, Vector3 point, int interval)
        {
            unit.Agent.SetDestination(point);
            while (isSetDestination && Application.isPlaying)
            {
                await Task.Delay(interval);
            }
        }

        private async Task SetDestinationAsync(UnitBase unit, IEntity target, int interval)
        {
            while (isSetDestination && Application.isPlaying)
            {
                unit.Agent.SetDestination(target.Position);
                await Task.Delay(interval);
            }
        }

        public override void Exit()
        {
            isSetDestination = false;
        }
    }
}