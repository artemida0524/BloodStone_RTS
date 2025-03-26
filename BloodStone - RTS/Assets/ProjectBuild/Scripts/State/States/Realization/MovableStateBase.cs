using Entity;
using System.Threading.Tasks;
using Unit;
using UnityEngine;

namespace State
{
    // CHANGE STATE
    public abstract class MovableStateBase : StateBase
    {
        protected bool isSetDestination = true;

        protected async void SetDestinationAsyncRunner(UnitBase unit, EntityBase target, int interval = 100)
        {
            await SetDestinationAsync(unit, target, interval);
        }

        protected async void SetDestinationAsyncRunner(UnitBase unit, Vector3 point, int interval = 100)
        {
            await SetDestinationAsync(unit, point, interval);
        }

        private async Task SetDestinationAsync(UnitBase unit, EntityBase target, int interval)
        {
            await SetDestinationAsync(unit, target.Position, interval);
        }

        private async Task SetDestinationAsync(UnitBase unit, Vector3 point, int interval)
        {
            while (isSetDestination && Application.isPlaying)
            {
                unit.Agent.SetDestination(point);
                await Task.Delay(interval);
            }
        }
        public override void Exit()
        {
            isSetDestination = false;
        }
    }
}