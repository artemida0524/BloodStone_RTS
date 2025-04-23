using Entity;
using State;
using System;
using UnityEngine;
using Weapon;

namespace Unit
{
    [RequireComponent(typeof(AnimationEventCallBackAttack))]
    public abstract class AttackingUnitBase : UnitBase
    {
        [field: SerializeField]
        public Transform LeftTargetWeapon { get; protected set; }

        [field: SerializeField]
        public Transform RightTargetWeapon { get; protected set; }

        [field: SerializeField]
        public AnimationEventCallBackAttack AnimationEventCallBalck { get; protected set; }

        [SerializeField]
        protected WeaponBase beginWeapon;

        protected WeaponBase _currentWeapon;
        public WeaponBase CurrentWeapon
        {
            get
            {
                if (_currentWeapon == null)
                {
                    SetWeapon(beginWeapon);
                    
                }
                return _currentWeapon;
            }

            protected set
            {
                _currentWeapon = value;
                OnWeaponChanged?.Invoke(_currentWeapon);
            }
        }

        public event Action<WeaponBase> OnWeaponChanged;

        public override string IdleAnimation
        {
            get
            {
                return CurrentWeapon.IdleAnimation;
            }
            protected set { }
        }
        public override string WalkingAnimation
        {
            get
            {
                return CurrentWeapon.WalkingAnimation;
            }
            protected set { }
        }
        public override string RunningAnimation
        {
            get
            {
                return CurrentWeapon.RunningAnimation;
            }
            protected set { }
        }

        protected override void Start()
        {
            base.Start();

            if (_currentWeapon == null)
            {
                InitializationWeapon();
            }
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

        protected void ResetWeapon()
        {
            Destroy(_currentWeapon);
            _currentWeapon = null;
        }

        protected void SetWeapon(WeaponBase weapon)
        {

            if (_currentWeapon != null)
            {
                ResetWeapon();
            }

            switch (weapon.weaponLocation)
            {
                case WeaponLocation.LeftHand:
                    CurrentWeapon = Instantiate(weapon, LeftTargetWeapon);
                    break;
                case WeaponLocation.RightHand:
                    CurrentWeapon = Instantiate(weapon, RightTargetWeapon);
                    break;
            }
            _currentWeapon.Unit = this;
            _currentWeapon.transform.localPosition = Vector3.zero;
        }

        public bool CanShoot()
        {
            return beginWeapon.CanShoot();
        }

        public void Shoot(EntityBase enemyEntity)
        {
            beginWeapon.Shoot(enemyEntity);
        }


        protected void InitializationWeapon()
        {
            SetWeapon(beginWeapon);
        }
    }
}
