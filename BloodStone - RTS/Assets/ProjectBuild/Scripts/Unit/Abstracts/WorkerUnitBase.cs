using Option;
using State;

namespace Unit
{
    public class WorkerUnitBase : UnitBase
    {

        public override IOption InitOption()
        {
            return new OptionWorkerUnit(this);
        }

        protected override StateBehaviourBase InitializeState()
        {
            return new WorkerStateBehaviour(this);
        }
    }
}