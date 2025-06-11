using Game.Gameplay.Entity;

namespace Game.Gameplay.Units
{

    public interface IUnit : IEntity
    {
        int HousingCost { get; }
    }
}