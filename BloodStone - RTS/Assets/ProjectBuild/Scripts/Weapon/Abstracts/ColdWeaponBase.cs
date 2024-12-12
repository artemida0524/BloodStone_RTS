
using System;
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
                    currentUnit.OnShootDetect -= OnShootDetect;
                    currentUnit.OnCreateBullet -= OnCreateBullet;
                }

                currentUnit = value;

                currentUnit.OnShootDetect += OnShootDetect;
                currentUnit.OnCreateBullet += OnCreateBullet;
            }
        }


        protected virtual void OnShootDetect() { }
        protected virtual void OnCreateBullet() { }


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
                    currentUnit.OnShootDetect -= OnShootDetect; 
                }

                currentUnit = value;

                currentUnit.OnShootDetect += OnShootDetect;
            }
        }

        protected abstract void OnShootDetect();
    }
}
