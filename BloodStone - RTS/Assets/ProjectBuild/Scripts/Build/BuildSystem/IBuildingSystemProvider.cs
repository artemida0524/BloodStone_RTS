using UnityEngine;

namespace Build
{
    public interface IBuildingSystemProvider
    {

        void Init();

        void SetBuild(BuildBase instance);
        void PlaceBuilding(BuildBase instance, BuildType type, Vector3 position);
    }
}