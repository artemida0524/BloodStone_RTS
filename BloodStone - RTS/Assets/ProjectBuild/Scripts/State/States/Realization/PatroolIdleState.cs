﻿using Unit;
using UnityEngine;

namespace State
{
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
            Collider[] colliders = Physics.OverlapSphere(unit.transform.position, unit.StateInteractable.radius);

            if (colliders.Length > 0)
            {
                
                foreach (var item in colliders)
                {
                    if(item.TryGetComponent(out UnitBase unit))
                    {
                        if(unit.FactionType != this.unit.FactionType)
                        {
                            this.unit.StateInteractable.SetState(new AttackAndFollowState(this.unit, unit));
                            return;
                        }
                    }
                }

            }

        }


    }

}