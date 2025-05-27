using Game.Gameplay.Stats;
using System;
using UnityEngine;

namespace Bar
{
    public abstract class UIStatsViewBase : MonoBehaviour, IDisposable
    {
        public IStat Stat { get; protected set; }

        public abstract void Init(IStat stat, UIBarDataAsset rowBarAsset);
        public abstract void Dispose();
    }
}