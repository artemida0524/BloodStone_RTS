using UnityEngine;

namespace Unit
{
    public interface ISelectable
    {
        Vector3 Position { get; }
        bool IsSelection { get; }
        bool CanSelected { get; }

        bool Select();
        void Unselect();
    }
}