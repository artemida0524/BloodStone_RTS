using Option;
using Entity;

namespace Select
{
    public interface ISelectable : IEntity
    {
        bool IsSelection { get; }
        bool CanSelected { get; }
        IOption Options { get; }
        EntityInfoSO EntityInfo { get; }

        bool Select();
        void Unselect();
    }
}