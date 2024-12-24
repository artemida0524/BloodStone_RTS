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
                }
                currentUnit = value;

                currentUnit.AnimationEventHandler.OnShootDetect += OnShootDetect;
                currentUnit.AnimationEventHandler.OnCreateBullet += OnCreateBullet;
            }
        }

        protected virtual void OnShootDetect() { Debug.LogWarning("NotImplemented"); }
        protected virtual void OnCreateBullet() { Debug.LogWarning("NotImplemented"); }
    }

    public abstract class ColdWeaponBase : WeaponBase
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
                }

                currentUnit = value;

                currentUnit.AnimationEventHandler.OnShootDetect += OnShootDetect;
            }
        }

        protected abstract void OnShootDetect();
    }
}
