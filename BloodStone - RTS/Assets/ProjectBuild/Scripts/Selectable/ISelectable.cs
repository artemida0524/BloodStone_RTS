using BloodStone.Gameplay.Entity;
using BloodStone.Gameplay.Options;

namespace BloodStone.Gameplay.Selection
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