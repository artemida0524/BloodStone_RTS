using Scripts.ObjectPool.Implementation;
using Scripts.ObjectPool.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ObjectPool.Abstract
{
    public abstract class BasePoolObjects : MonoBehaviour, IPool
    {
        [SerializeField]
        private string _key;

        [SerializeField] private List<IPoolObject> _pulledObjects = new List<IPoolObject>();

        public abstract event EventHandler<IPoolObject> OnPushed;
        public abstract event EventHandler<IPoolObject> OnPulled;

        public string Key { get => _key; }
        public List<IPoolObject> PulledObjects { get => _pulledObjects; }

        public abstract void Initialize();
        public abstract IPoolObject Pull();
        public abstract void Push(IPoolObject poolObject);
        public abstract bool IsHasObject();

        protected virtual void AddPulledObjects(IPoolObject poolObject)
        {
            _pulledObjects.Add(poolObject as PoolObject);
        }

        protected virtual void RemovePulledObjects(IPoolObject poolObject)
        {
            _pulledObjects.Remove(poolObject as PoolObject);
        }

        public abstract IPoolObject GetOriginalObject();
    }
}
