using Pool;
using System.Collections.Generic;
using UnityEngine;

public class PoolProviderTest : MonoBehaviour
{
    [SerializeField] private List<BasePoolObjects> _pools;


    private void Awake()
    {
        _pools.ForEach(x => x.Initialize());
    }

    public IPoolObject Pull(string key)
    {
        BasePoolObjects poolObjects = _pools.Find(x => x.Key == key);

        return poolObjects.Pull();
    }
}