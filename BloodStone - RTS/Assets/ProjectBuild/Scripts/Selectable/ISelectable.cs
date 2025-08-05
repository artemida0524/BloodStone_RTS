using Game.Gameplay.Entity;
using Game.Gameplay.Options;

namespace Game.Gameplay.Selection
{
    public interface ISelectable : IEntity
    {
        bool IsSelected { get; }
        bool CanSelected { get; }
        IOption Options { get; }
        EntityInfoSO EntityInfo { get; }

        bool Select();
        void Unselect();
    }
}