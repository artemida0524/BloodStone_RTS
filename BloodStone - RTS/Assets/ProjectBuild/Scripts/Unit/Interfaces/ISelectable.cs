using UnityEngine;
using UnityEngine.Rendering.LookDev;

namespace Unit
{
    public interface ISelectable
    {
        Vector3 Position { get; }
        bool IsSelection { get; }
        bool CanSelected { get; }
        Sprite Sprite { get; } 
        IInteractable SelectInfo { get; }

        bool Select();
        void Unselect();
    }
}