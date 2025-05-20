using Select;
using State;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

namespace Build
{
    public partial class GenerateNewSimpleUnitTower : BuildInteractableBase
    {
        [SerializeField] private Transform pointSpawn;
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Mesh breakBuild;
        [SerializeField] private Mesh buildMesh;

        private PoolProviderTest _poolProvider;

        [Inject]
        private void Construct(PoolProviderTest poolProvider)
        {
            _poolProvider = poolProvider;
        }

        protected override void Update()
        {
            base.Update();

            if(Machine.State == null)
            {
                return;
            }

            if (Machine.State.IsFinished)
            {
                switch (Machine.State)
                {
                    case NotBuildState:
                        meshFilter.mesh = buildMesh;
                        Machine.ChangeState(new BuildWorkingState(this, _poolProvider, pointSpawn));
                        BuildType = BuildType.Built;
                        break;
                }
            }
        }

        protected override void ChangeStateByBuildType(BuildType type)
        {
            base.ChangeStateByBuildType(type);

            switch (type)
            {
                case BuildType.NotBuilt:
                    Debug.Log("Build");
                    meshFilter.mesh = breakBuild;
                    Machine.ChangeState(new NotBuildState(this));
                    break;

                case BuildType.Built:
                    meshFilter.mesh = buildMesh;
                    Machine.ChangeState(new BuildWorkingState(this, _poolProvider, pointSpawn));
                    break;

                case BuildType.Broken:

                    break;
            }

        }
    }
}