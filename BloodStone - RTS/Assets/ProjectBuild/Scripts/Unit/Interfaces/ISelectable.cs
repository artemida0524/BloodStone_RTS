using UnityEngine;
using UnityEngine.Rendering.LookDev;
using Option;

namespace Unit
{
    public interface ISelectable
    {
        Vector3 Position { get; }
        bool IsSelection { get; }
        bool CanSelected { get; }
        EntityInfoSO EntityInfo { get; }
        IOption Options { get; }

        bool Select();
        void Unselect();
    }
}