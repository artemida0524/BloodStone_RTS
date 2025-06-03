using Scripts.ObjectPool.Abstract;
using Scripts.ObjectPool.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ObjectPool.Provider
{
    public class PoolProviderTest : MonoBehaviour
    {
        [SerializeField] private List<BasePoolObjects> _pools;

        private Dictionary<string, BasePoolObjects> _poolDictionary;

        public void Init()
        {
            _poolDictionary = new Dictionary<string, BasePoolObjects>();

            foreach (var pool in _pools)
            {
                pool.Initialize();

                if (!_poolDictionary.ContainsKey(pool.Key))
                {
                    _poolDictionary.Add(pool.Key, pool);
                }
                else
                {
                    Debug.Log($"Pool exists with the same key: {pool.Key}");
                }
            }
        }

        public IPoolObject Pull(string key)
        {
            if (_poolDictionary.TryGetValue(key, out var pool))
            {
                return pool.Pull();
            }

            Debug.LogError($"Pool not found with key: {key}");
            return null;
        }


        public BasePoolObjects GetPullByKey(string key)
        {
            return _poolDictionary.TryGetValue(key, out var pool) ? pool : null;
        }
    } 
}