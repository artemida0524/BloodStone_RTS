using Scripts.ObjectPool.Abstract;
using Scripts.ObjectPool.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
namespace Scripts.ObjectPool.Implementation
{

    public class PoolObjectsTest : BasePoolObjects
    {
        [SerializeField]
        private PoolObject _originalObject;
        [SerializeField]
        private int _startSizePool;
        [SerializeField]
        private Transform _container;

        private DiContainer _diContainer;

        public override event EventHandler<IPoolObject> OnPushed;
        public override event EventHandler<IPoolObject> OnPulled;


        private List<PoolObject> _activeObjects = new List<PoolObject>();

        public PoolObject OriginalObject { get => _originalObject; }

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }


        private void Reset()
        {
            _container = transform;
        }

        public virtual void Initialize(PoolObject originalObject)
        {
            SetOriginalObject(originalObject);
            Initialize();
        }

        public override void Initialize()
        { 
            for (int i = 0; i < _startSizePool; i++)
            {
                PoolObject obj = CreatePoolObject();
                obj.GetOwner().SetActive(false);
                AddPulledObjects(obj);
            }
        }

        public override void Push(IPoolObject poolObject)
        {
            if (poolObject == null || poolObject.GetOwner() == null)
            {
                return;
            }

            if (this._activeObjects.Contains(poolObject as PoolObject) )
            {
                AddPulledObjects(poolObject as PoolObject);

                poolObject.GetOwner().SetActive(false);
                poolObject.GetOwner().transform.SetParent(_container.transform);
                this._activeObjects.Remove(poolObject as PoolObject);
                OnPushed?.Invoke(this, poolObject);
            }
        }
        public override IPoolObject Pull()
        {
            IPoolObject result = null;
            if (IsHasObject(out PoolObject obj))
            {
                result = obj;
            }
            else
            {
                result = CreatePoolObject();
            }
            result.Initialize(this);
            result.GetOwner().transform.SetParent(null);
            RemovePulledObjects(result as PoolObject);
            _activeObjects.Add(result as PoolObject);
            OnPulled?.Invoke(this, result);

            return result;
        }

        private PoolObject CreatePoolObject()
        {
            PoolObject spawned = InstantiateObject(_originalObject, _container.transform);
            return spawned;
        }

        protected virtual PoolObject InstantiateObject(PoolObject prefab, Transform parent)
        {
            PoolObject result = null;
            try
            {
                result = _diContainer.InstantiatePrefab(prefab, parent).GetComponent<PoolObject>();
            }
            catch (Exception)
            {
                result = Instantiate(prefab, parent);
            }
            result.Initialize(this);
            return result;
        }

        protected void SetOriginalObject(PoolObject originalObject)
        {
            _originalObject = originalObject;
        }

        protected void SetSizePool(int value)
        {
            _startSizePool = value;
        }

        public override bool IsHasObject()
        {
            return PulledObjects.Any(x => !x.GetOwner().activeSelf);
        }

        private bool IsHasObject(out PoolObject result)
        {
            bool has = false;
            PoolObject poolObj = null;

            if(this.PulledObjects.Count > 0)
            {
                result = this.PulledObjects[0] as PoolObject;
                return true;
            }

            result = poolObj;
            return has;
        }

        public override IPoolObject GetOriginalObject()
        {
            return _originalObject;
        }
    }

}