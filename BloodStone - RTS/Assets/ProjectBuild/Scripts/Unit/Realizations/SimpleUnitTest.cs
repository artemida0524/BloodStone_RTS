using Bar;
using UnityEngine;

namespace Unit
{
    public class SimpleUnitTest : SimpleUnitBase
    {
        [field: SerializeField] public override Sprite Sprite { get; protected set; }
    }
}