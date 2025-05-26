using Game.Gameplay.Build.Providers;
using Game.Gameplay.Units.Providers;
using GlobalData;
using Scripts.ObjectPool.Provider;
using Select;
using UnityEngine;
using Zenject;
namespace Scripts.EntryPoint
{

    public class GameplayEntryPoint : MonoBehaviour
    {
        private SelectableHandler _selectableHandler;
        private SelectedController _selectedController;
        private IBuildingProvider _buildingProvider;
        private IBuildingSystemProvider _buildingSystemProvider;
        private IUnitProvider _unitProvider;
        private PoolProviderTest _poolProviderTest;


        private BuildingInitializer _buildingInitializer;
        private UnitInitializer _unitInitializer;


        [Inject]
        private void Construct(SelectableHandler selectableHandler, SelectedController selectedController, IBuildingProvider buildingProvider, IBuildingSystemProvider buildingSystemProvider, IUnitProvider unitProvider, PoolProviderTest poolProvider)
        {
            _selectableHandler = selectableHandler;
            _selectedController = selectedController;
            _buildingProvider = buildingProvider;
            _buildingSystemProvider = buildingSystemProvider;
            _unitProvider = unitProvider;
            _poolProviderTest = poolProvider;
        }


        private void Awake()
        {
            _selectableHandler.Init();
            _selectedController.Init();

            _poolProviderTest.Init();

            _buildingProvider.Init();
            _buildingSystemProvider.Init();

            _unitProvider.Init();
            Debug.Log("unitproviderInit");

            _buildingInitializer = new BuildingInitializer(_buildingSystemProvider);
            _buildingInitializer.Init();

            _unitInitializer = new UnitInitializer(_unitProvider);
            _unitInitializer.Init();

        }
    } 
}