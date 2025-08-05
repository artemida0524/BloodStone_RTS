using System;
using UnityEngine;

namespace Game.Gameplay.Units
{

    public class WearWorkerUnit : WorkerUnitBase
    {
        [field: SerializeField] public int Amount { get; protected set; } = 30;
    }

}