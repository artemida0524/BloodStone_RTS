using Entity;
using State;
using System;
using UnityEngine;
using Weapon;

namespace Unit
{
    public abstract class AttackingUnitBase : UnitBase
    {
        [field: SerializeField] public Transform LeftTargetWeapon { get; protected set; }
        [field: SerializeField] public Transform RightTargetWeapon { get; protected set; }
        [field: SerializeField] public WeaponBase Weapon { get; protected set; }
        [field: SerializeField] public WeaponBase Melle { get; protected set; }

        protected bool alreadyInitialized = false;

        public override string IdleAnimation
        {
            get
            {
                if (!alreadyInitialized)
                {
                    InitializationWeapon();
                    alreadyInitialized = true;
                }

                return Weapon.IdleAnimation;

            }
            protected set { }
        }
        public override string WalkingAnimation
        {
            get
            {
                if (!alreadyInitialized)
                {
                    InitializationWeapon();
                    alreadyInitialized = true;
                }

                return Weapon.WalkingAnimation;
            }
            protected set { }
        }
        public override string RunningAnimation
        {
            get
            {
                if (!alreadyInitialized)
                {
                    InitializationWeapon();
                    alreadyInitialized = true;
                }

                return Weapon.RunningAnimation;
            }
            protected set { }
        }

        public event Action OnShootDetect;
        public event Action OnCreateBullet;

        public void ShootAnimationEventCallBack()
        {
            OnShootDetect?.Invoke();
        }

        public void CreateBulletAnimationEventCallBack()
        {
            OnCreateBullet?.Invoke();
        }

        protected override StateBehaviourBase InitializeState()
        {
            return new AttackingBehaviour(this);
        }


        public override void MoveTo(Vector3 point)
        {
            StateInteractable.SetState(new RunningState(this, point));
        }

        protected void SetWeapon(WeaponBase weapon)
        {


            switch (weapon.weaponLocation)
            {
                case WeaponLocation.LeftHand:
                    Weapon = Instantiate(weapon, LeftTargetWeapon);
                    break;
                case WeaponLocation.RightHand:
                    Weapon = Instantiate(weapon, RightTargetWeapon);
                    break;
            }

            //Weapon = Instantiate(weapon, RightTargetWeapon);
            Weapon.transform.localPosition = Vector3.zero;
        }

        public bool CanShoot()
        {
            return Weapon.CanShoot();
        }

        public void Shoot(EntityBase enemyEntity)
        {
            Weapon.Shoot(enemyEntity);
        }


        protected void InitializationWeapon()
        {

            if (Weapon != null)
            {
                SetWeapon(Weapon);
            }
            else
            {
                SetWeapon(Melle);
            }


            Weapon.Unit = this;

        }



    }
}
