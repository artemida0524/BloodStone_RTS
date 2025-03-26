using State;

namespace Unit
{
    public class WorkerUnitBase : UnitBase
    {
        protected override StateBehaviourBase InitializeState()
        {
            return new WorkerStateBehaviour(this);
        }
    }
}