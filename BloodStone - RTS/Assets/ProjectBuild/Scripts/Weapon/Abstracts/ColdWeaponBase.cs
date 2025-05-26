using Game.Gameplay.Units;
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
                    currentUnit.AnimationEventCallBalck.OnShootDetect -= OnShootDetect;
                }

                currentUnit = value;

                currentUnit.AnimationEventCallBalck.OnShootDetect += OnShootDetect;
            }
        }

        protected abstract void OnShootDetect();
    }
}
