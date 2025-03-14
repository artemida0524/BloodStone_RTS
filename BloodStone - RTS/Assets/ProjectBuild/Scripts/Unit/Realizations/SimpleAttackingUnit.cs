
using UnityEngine;

namespace Unit
{
    public class SimpleAttackingUnit : AttackingUnitBase
    {
        [field: SerializeField] public override Sprite Sprite { get; protected set; }
    }
}
