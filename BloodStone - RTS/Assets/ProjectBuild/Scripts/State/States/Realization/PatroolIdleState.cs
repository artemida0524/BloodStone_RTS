using Game.Gameplay;
using Game.Gameplay.Units;
using Game.Gameplay.Entity;
using Unit;
using UnityEngine;

namespace State
{
    //CHANGE STATE 2
    public class PatroolIdleState : StateBase
    {
        private readonly AttackingUnitBase unit;

        public PatroolIdleState(AttackingUnitBase unit)
        {
            this.unit = unit;
        }

        public override void Enter()
        {
            unit.Animator.Play(unit.IdleAnimation);
        }

        public override void Update()
        {

            Collider[] colliders = Physics.OverlapSphere(unit.transform.position, unit.StateInteractable.Radius);

            if (colliders.Length > 0)
            {
                foreach (var item in colliders)
                {
                    if (item.TryGetComponent(out IDamageable entity))
                    {
                        if (entity.FactionType != this.unit.FactionType && entity.FactionType != Faction.FactionType.Systems)
                        {
                            this.unit.SetState(new AttackAndFollowState(this.unit, entity));
                            return;
                        }
                    }
                }
            }

        }
    }
}