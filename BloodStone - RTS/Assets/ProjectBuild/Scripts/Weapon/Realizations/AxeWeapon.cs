using Entity;
using UnityEngine;

namespace Weapon
{
    public class AxeWeapon : ColdWeaponBase
    {
        public override string AttackAnimation { get; protected set; } = AnimationStateNames.DOWNWARD_ATTACK;
        public override string IdleAnimation { get; protected set; } = AnimationStateNames.IDLE_AXE;
        public override string WalkingAnimation { get; protected set; } = AnimationStateNames.WALKING;
        public override string RunningAnimation { get; protected set; } = AnimationStateNames.RUNNING;

        protected override void Update()
        {
            base.Update();
        }


        public override void Shoot(EntityBase enemyEntity)
        {
            base.Shoot(enemyEntity);

            Unit.Animator.Play(AttackAnimation, 0, 0);
        }

        protected override void OnShootDetect()
        {
            Debug.Log("efwefwe");
        }



    }
}