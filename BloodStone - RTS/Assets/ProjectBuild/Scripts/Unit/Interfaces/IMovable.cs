using UnityEngine;

namespace Game.Gameplay
{
    public interface IMovable
    {
        bool CanMove { get; }
        
        bool MoveTo(Vector3 point, float radius);
        void ResetMove();
    }
}