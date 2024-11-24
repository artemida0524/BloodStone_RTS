using UnityEngine;

namespace Unit
{
    public interface IMovable
    {
        bool CanMove { get; }

        void MoveTo(Vector3 point);
    }
}