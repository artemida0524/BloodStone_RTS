using System;
using UnityEngine;

namespace Bar
{
    public abstract class UIBarViewBase : MonoBehaviour, IDisposable
    {
        public IStats ResourceStat { get; protected set; }

        public abstract void Init(IStats resourceBar, UIBarDataAsset rowBarAsset);
        public abstract void Dispose();
    }
}