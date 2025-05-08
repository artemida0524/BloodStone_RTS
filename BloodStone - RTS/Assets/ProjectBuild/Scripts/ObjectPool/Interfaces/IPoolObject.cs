using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pool
{
    public interface IPoolObject
    {
        event EventHandler<IPoolObject> OnPushed;

        IPool Pool { get; }
        void Initialize(IPool pool);
        void Push();

        T GetOwner<T>();
        GameObject GetOwner();
    }
}
