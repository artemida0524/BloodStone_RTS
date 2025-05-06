using Select;
using State;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Build
{
    public class GenerateNewSimpleUnitTower : BuildInteractableBase
    {
        [SerializeField] private Transform pointSpawn;
        [SerializeField] private SimpleUnitBase unitPrefab;

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
                        Machine.ChangeState(new BuildWorkingState(this, unitPrefab, pointSpawn));
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
                    Machine.ChangeState(new BuildWorkingState(this, unitPrefab, pointSpawn));
                    break;

                case BuildType.Broken:

                    break;
            }

        }

        public override void Interact()
        {
            Debug.Log("Just");
        }

        private class BuildWorkingState : StateBase
        {
            private readonly GenerateNewSimpleUnitTower _build;

            private Transform pointSpawn;
            private SimpleUnitBase unitPrefab;

            private const float timeOut = 10f;
            private float time = 0f;

            public BuildWorkingState(GenerateNewSimpleUnitTower build, SimpleUnitBase unitPrefab, Transform pointSpawn)
            {
                _build = build;
                this.unitPrefab = unitPrefab;
                this.pointSpawn = pointSpawn;
            }

            public override void Enter()
            {
                base.Enter();
            }

            public override void Update()
            {
                base.Update();


                Debug.Log(time);

                if (!_build.CanInteraction)
                {
                    return;
                }

                time += Time.deltaTime;
                if (time > timeOut)
                {
                    time = 0f;

                    SimpleUnitBase unit = Instantiate(unitPrefab, pointSpawn.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
                    unit.InitializationEntity(_build.FactionType);
                }
            }
        }
    }
}