using Game.Gameplay.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Melle : WeaponBase
    {
        public override string AttackAnimation
        {
            get
            {
                List<string> listBox = new List<string>()
                {
                    AnimationStateNames.BOX1,
                    AnimationStateNames.BOX2,
                    AnimationStateNames.BOX3,
                };

                int random = Random.Range(0, listBox.Count);

                return listBox[random];
            }
            protected set
            {

            }
        }

        public override string IdleAnimation { get; protected set; } = AnimationStateNames.IDLE;
        public override string WalkingAnimation { get; protected set;} = AnimationStateNames.WALKING;
        public override string RunningAnimation { get; protected set; } = AnimationStateNames.RUNNING;

        public override void BeginAttack()
        {
            AttackRightNow = true;
        }

        public override void EndAttack()
        {
            AttackRightNow = false;
        }

        public override void Shoot(EntityBase enemyEntity)
        {
            base.Shoot(enemyEntity);


            Unit.Animator.Play(AttackAnimation, 0, 0);

        }

    }
}
