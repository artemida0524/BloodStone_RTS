using UnityEngine;

namespace Unit
{
    public class SimpleUnit : SimpleUnitBase
    {
        [field: SerializeField] public override Sprite Sprite { get; protected set; }
    }
}