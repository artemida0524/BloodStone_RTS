using Faction;
using GlobalData;
using UnityEngine;
using Zenject;

namespace Build
{
    public class BuildingSystem : MonoBehaviour, IBuildingSystemProvider
    {

        [SerializeField] private LayerMask mask;

        new private Camera camera;

        private Faction _faction;
        private BuildBase _currentBuild;

        private GlobalBuildsDataHandler _buildsData;
        private Vector3Int _lastPosition = new Vector3Int(0, 0, 0);

        [Inject]
        private void Construct(Faction faction, GlobalBuildsDataHandler globalBuildsDataHandler)
        {
            _faction = faction;
            _buildsData = globalBuildsDataHandler;
        }


        

        public void Init()
        {
            camera = Camera.main;
        }

        private void Update()
        {
            if (_currentBuild)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, mask))
                {
                    Vector3Int newPos = new Vector3Int((int)hitInfo.point.x, 0, (int)hitInfo.point.z);

                    if(_lastPosition != newPos)
                    {
                        _lastPosition = newPos;
                        _currentBuild.transform.position = newPos;

                        if (_buildsData.GlobalBuildsGridData.CanBuildInGrid(_currentBuild, new Vector3Int((int)hitInfo.point.x, (int)hitInfo.point.y, (int)hitInfo.point.z)))
                        {
                            _currentBuild.SetColor(Color.green);
                        }
                        else
                        {
                            _currentBuild.SetColor(Color.red);
                        }
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_buildsData.GlobalBuildsGridData.CanBuildInGrid(_currentBuild, new Vector3Int((int)hitInfo.point.x, (int)hitInfo.point.y, (int)hitInfo.point.z)))
                        {
                            _buildsData.AddBuild(_currentBuild);
                            _faction.Data.ChangeInteractionMode(InteractionMode.None);
                            _currentBuild.Unvisualize();
                            _currentBuild.Build(BuildType.NotBuilt);
                            _currentBuild = null; 
                        }
                    }
                }
            }
        }

        private void OnDisable()
        {
            _faction.Data.ChangeInteractionMode(InteractionMode.None);
        }

        public void SetBuild(BuildBase instance)
        {
            if (!_currentBuild)
            {
                _currentBuild = instance;
                _faction.Data.ChangeInteractionMode(InteractionMode.Build);
                _currentBuild.Visualize();
            }
        }

        public void PlaceBuilding(BuildBase instance, BuildType type, Vector3 position)
        {
            instance.transform.position = position;
            _buildsData.AddBuild(instance);
            instance.Build(type);
        }
    }
}