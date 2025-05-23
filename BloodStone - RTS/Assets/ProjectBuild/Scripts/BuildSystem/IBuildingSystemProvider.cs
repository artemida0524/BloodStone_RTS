using BloodStone.Gameplay.Build;
using UnityEngine;

namespace BloodStone.Gameplay.Build.Providers
{
    public interface IBuildingSystemProvider
    {

        void Init();

        void SetBuild(BuildBase instance);
        void PlaceBuilding(BuildBase instance, BuildType type, Vector3 position);
    }
}