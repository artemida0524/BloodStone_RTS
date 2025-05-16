using System;
using UnityEngine;

namespace Bar
{
    public abstract class UIBarViewBase : MonoBehaviour, IDisposable
    {
        public IStats Stat { get; protected set; }

        public abstract void Init(IStats stats, UIBarDataAsset rowBarAsset);
        public abstract void Dispose();
    }
}