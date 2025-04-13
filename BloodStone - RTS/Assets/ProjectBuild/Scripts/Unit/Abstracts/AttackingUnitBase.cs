using Entity;
using State;
using UnityEngine;
using Weapon;

namespace Unit
{
    [RequireComponent(typeof(AnimationEventHandler))]
    public abstract class AttackingUnitBase : UnitBase
    {
        [field: SerializeField] public Transform LeftTargetWeapon { get; protected set; }
        [field: SerializeField] public Transform RightTargetWeapon { get; protected set; }

        [field: SerializeField] public WeaponBase CurrentWeapon { get; protected set; }
        [field: SerializeField] public WeaponBase Melle { get; protected set; }
        [field: SerializeField] public AnimationEventHandler AnimationEventHandler { get; protected set; }

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

                return CurrentWeapon.IdleAnimation;

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

                return CurrentWeapon.WalkingAnimation;
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

                return CurrentWeapon.RunningAnimation;
            }
            protected set { }
        }


        protected override void Update()
        {
            base.Update();
            //Debug.Log(StateInteractable.Behaviour.StateMachine.State + " " + name);
        }

        protected override StateBehaviourBase InitializeState()
        {
            return new AttackingBehaviour(this);
        }

        public override bool MoveTo(Vector3 point, float radius)
        {
            if (CanMove)
            {
                StateInteractable.MoveState.ChangeState(new RunningState(this, point, radius));
            }
            return CanMove;
        }

        protected void SetWeapon(WeaponBase weapon)
        {
            switch (weapon.weaponLocation)
            {
                case WeaponLocation.LeftHand:
                    CurrentWeapon = Instantiate(weapon, LeftTargetWeapon);
                    break;
                case WeaponLocation.RightHand:
                    CurrentWeapon = Instantiate(weapon, RightTargetWeapon);
                    break;
            }
            CurrentWeapon.transform.localPosition = Vector3.zero;
        }

        public bool CanShoot()
        {
            return CurrentWeapon.CanShoot();
        }

        public void Shoot(EntityBase enemyEntity)
        {
            CurrentWeapon.Shoot(enemyEntity);
        }


        protected void InitializationWeapon()
        {

            if (CurrentWeapon != null)
            {
                SetWeapon(CurrentWeapon);
            }
            else
            {
                SetWeapon(Melle);
            }


            CurrentWeapon.Unit = this;

        }



    }
}
