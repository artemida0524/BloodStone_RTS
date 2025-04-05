using Unit;
using UnityEngine;

namespace Weapon
{
    public abstract class FirearmBase : WeaponBase
    {
        public override AttackingUnitBase Unit
        {
            get
            {
                return currentUnit;
            }
            set
            {
                if (currentUnit != null)
                {
                    currentUnit.AnimationEventHandler.OnShootDetect -= OnShootDetect;
                    currentUnit.AnimationEventHandler.OnCreateBullet -= OnCreateBullet;
                    currentUnit.AnimationEventHandler.OnBeginAttack -= BeginAttack;
                    currentUnit.AnimationEventHandler.OnEndAttack -= EndAttack;
                }
                currentUnit = value;

                currentUnit.AnimationEventHandler.OnShootDetect += OnShootDetect;
                currentUnit.AnimationEventHandler.OnCreateBullet += OnCreateBullet;
                currentUnit.AnimationEventHandler.OnBeginAttack += BeginAttack;
                currentUnit.AnimationEventHandler.OnEndAttack += EndAttack;
            }
        }

        protected virtual void OnShootDetect() { Debug.LogWarning("NotImplemented"); }
        protected virtual void OnCreateBullet() { Debug.LogWarning("NotImplemented"); }

        public override void BeginAttack()
        {
            AttackRightNow = true;
        }


        public override void EndAttack()
        {
            AttackRightNow = false;
        }
    }

}