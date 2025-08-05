using Scripts.ObjectPool.Interface;
using System;

namespace Scripts.ObjectPool.Implementation
{

    public class PoolObjectEntity : PoolObject
    {
        public event Action OnInitialize;
        public override void Initialize(IPool pool)
        {
            base.Initialize(pool);
            OnInitialize?.Invoke();
        }
    }
}