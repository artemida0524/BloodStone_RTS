using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public interface IInteractable
    {
        Sprite Icon { get; }
        List<DoActionOption> Actions { get; }
    }
}