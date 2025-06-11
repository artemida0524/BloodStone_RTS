using Currency;
using UnityEngine;

namespace Game.Gameplay.Units
{
    public class ChopTreeWorkerUnit : WorkerUnitBase
    {
        public ICurrency TreeCurrency { get; private set; } = new CurrencyBase(StatsNames.TREE, 0, 30);
    }

}