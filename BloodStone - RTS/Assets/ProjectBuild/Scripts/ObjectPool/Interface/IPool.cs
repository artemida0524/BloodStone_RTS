using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.ObjectPool.Interface
{
    public interface IPool
    {
        event EventHandler<IPoolObject> OnPushed;
        event EventHandler<IPoolObject> OnPulled;

        void Push(IPoolObject poolObject);
        IPoolObject Pull();
        bool IsHasObject();
    }
}
