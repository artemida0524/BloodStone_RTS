using UnityEngine;
using UnityEngine.Rendering.LookDev;
using Option;
using Entity;

namespace Select
{
    public interface ISelectable : IEntity
    {
        //Vector3 Position { get; }
        bool IsSelection { get; }
        bool CanSelected { get; }
        IOption Options { get; }
        EntityInfoSO EntityInfo { get; }

        bool Select();
        void Unselect();
    }
}