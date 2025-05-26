using System;
using UnityEngine;

namespace Bar
{
    public abstract class UIBarViewBase : MonoBehaviour, IDisposable
    {
        public IBar Stat { get; protected set; }

        public abstract void Init(IBar stats, UIBarDataAsset rowBarAsset);
        public abstract void Dispose();
    }
}