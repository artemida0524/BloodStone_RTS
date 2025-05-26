using Game.Gameplay.Units;
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
                    currentUnit.AnimationEventCallBalck.OnShootDetect -= OnShootDetect;
                    currentUnit.AnimationEventCallBalck.OnCreateBullet -= OnCreateBullet;
                    currentUnit.AnimationEventCallBalck.OnBeginAttack -= BeginAttack;
                    currentUnit.AnimationEventCallBalck.OnEndAttack -= EndAttack;
                }
                currentUnit = value;

                currentUnit.AnimationEventCallBalck.OnShootDetect += OnShootDetect;
                currentUnit.AnimationEventCallBalck.OnCreateBullet += OnCreateBullet;
                currentUnit.AnimationEventCallBalck.OnBeginAttack += BeginAttack;
                currentUnit.AnimationEventCallBalck.OnEndAttack += EndAttack;
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