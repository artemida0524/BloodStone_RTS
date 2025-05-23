using BloodStone.Gameplay.Entity;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapon
{
    public class BowWeapon : FirearmBase
    {
        [SerializeField] private GameObject throwableObject;

        private EntityBase enemyEntity;

        private GameObject bullet;

        public override string IdleAnimation { get; protected set; } = AnimationStateNames.IDLE_AXE;
        public override string WalkingAnimation { get; protected set; } = AnimationStateNames.WALKING;
        public override string RunningAnimation { get; protected set; } = AnimationStateNames.RUNNING;
        public override string AttackAnimation { get; protected set; } = AnimationStateNames.ARROW_ATTACK;

        public override void Shoot(EntityBase enemyEntity)
        {
            base.Shoot(enemyEntity);

            this.enemyEntity = enemyEntity;

            Unit.Animator.Play(AttackAnimation, 0, 0);
        }

        protected override void OnShootDetect()
        {
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody>().AddForce((enemyEntity.Position - Unit.Position).normalized * 800);
        }

        protected override void OnCreateBullet()
        {
            bullet = Instantiate(throwableObject, Unit.RightTargetWeapon);


            bullet.transform.localPosition = Vector3.zero;

        }
    }
}
