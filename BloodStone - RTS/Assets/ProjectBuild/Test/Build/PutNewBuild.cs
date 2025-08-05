using Game.Gameplay.Build;
using Scripts.ObjectPool.Provider;
using UnityEngine;
using Zenject;
using Game.Gameplay.Build.Providers;

public class PutNewBuild : MonoBehaviour
{

    private IBuildingSystemProvider _buildingSystemProvider;
    private PoolProviderTest _poolProvider;

    [Inject]
    private void Construct(IBuildingSystemProvider buildingSystemProvider, PoolProviderTest provider)
    {
        _buildingSystemProvider = buildingSystemProvider;
        _poolProvider = provider;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            BuildBase build = _poolProvider.Pull(PoolsNames.HUT).GetOwner<BuildBase>();
            build.gameObject.SetActive(true);
            _buildingSystemProvider.SetBuild(build);
        }
    }
}