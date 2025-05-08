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
                    Machine.ChangeState(new NotBuildState(this));
                    break;

                case BuildType.Built:
                    Machine.ChangeState(new BuildWorkingState(this, _poolProvider, pointSpawn));
                    break;

                case BuildType.Broken:

                    break;
            }

        }

        public override void Interact()
        {
            Debug.Log("Just");
        }
    }
}