using Entity;
using Unit;
using UnityEngine;
using UnityEngine.Rendering.UI;

namespace Weapon
{

    public abstract class WeaponBase : MonoBehaviour
    {
        [field: SerializeField] public float Distance { get; protected set; }
        [field: SerializeField] public float CooldownTime { get; protected set; }
        [field: SerializeField] public bool CanShooting { get; protected set; }

        [field: SerializeField] public virtual WeaponLocation weaponLocation { get; protected set; } = WeaponLocation.RightHand;

        protected float cooldownOut = 0f; // Passed Time

        public abstract string IdleAnimation { get; protected set; }
        public abstract string WalkingAnimation { get; protected set; }
        public abstract string RunningAnimation { get; protected set; }
        public abstract string AttackAnimation { get; protected set; }

        protected AttackingUnitBase currentUnit;
        public virtual AttackingUnitBase Unit { get { return currentUnit; } set { currentUnit = value; } }

        public bool AttackRightNow { get; protected set; } = false;

        protected virtual void Update()
        {
            cooldownOut += Time.deltaTime;
            //Debug.Log(AttackRightNow);
        }
        public virtual void Shoot(EntityBase enemyEntity)
        {
            Debug.Log("Shoot");
            cooldownOut = 0f;
        }

        public virtual bool CanShoot()
        {
            return CanShooting && cooldownOut > CooldownTime;
        }

        public abstract void BeginAttack();
        public abstract void EndAttack();


    }
}
