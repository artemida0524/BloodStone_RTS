using System;

namespace Pool
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