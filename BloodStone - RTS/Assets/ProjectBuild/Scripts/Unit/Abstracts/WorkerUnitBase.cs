using UnityEngine;
using State;
using Zenject;
using GlobalData;
using BloodStone.Gameplay.Options;

namespace BloodStone.Gameplay.Units
{
    [RequireComponent(typeof(AnimationEventCallBackWoker))]
    public abstract class WorkerUnitBase : UnitBase
    {
        [field: SerializeField] public AnimationEventCallBackWoker AnimationEventCallBack { get; protected set; }

        private IBuildingProvider _buildingProvider;


        [Inject]
        private void Construct(IBuildingProvider buildingProvider)
        {
            _buildingProvider = buildingProvider;
        }


        public override bool MoveTo(Vector3 point, float radius)
        {
            if (CanMove)
            {
                StateInteractable.MoveState.ChangeState(new RunningState(this, point, radius));
            }
            return CanMove;
        }


        public override IOption InitOption()
        {
            return new OptionWorkerUnit(this, _buildingProvider);
        }

        protected override StateBehaviourBase InitializeState()
        {
            return new WorkerStateBehaviour(this);
        }
    }
}