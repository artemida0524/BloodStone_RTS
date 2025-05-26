using Currency;
using UnityEngine;

namespace Game.Gameplay.Units
{
    public class ChopTreeWorkerUnit : WorkerUnitBase
    {
        public ICurrency TreeCurrency { get; private set; } = new TreeCurrency(0, 30);

        protected override void Update()
        {
            base.Update();

            //Debug.Log(TreeCurrency.Count + " " + name);
        }
    }

}