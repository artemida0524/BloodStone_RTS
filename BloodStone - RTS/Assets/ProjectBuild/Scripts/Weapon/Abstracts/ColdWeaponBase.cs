using Unit;

namespace Weapon
{

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
