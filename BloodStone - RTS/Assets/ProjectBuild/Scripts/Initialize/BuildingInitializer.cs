using BloodStone.Gameplay.Build;
using BloodStone.Gameplay.Build.Providers;
using UnityEngine;

public class BuildingInitializer
{
    private readonly IBuildingSystemProvider _buildingSystemProvider;

    public BuildingInitializer(IBuildingSystemProvider provider)
    {
        _buildingSystemProvider = provider;
    }


    public void Init()
    {
        BuildBase[] builds = Object.FindObjectsOfType<BuildBase>();

        foreach (var item in builds)
        {
            _buildingSystemProvider.PlaceBuilding(item, BuildType.Built, item.transform.position);
        }
    }
}
