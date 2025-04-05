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
                    currentUnit.AnimationEventHandler.OnBeginAttack -= BeginAttack;
                    currentUnit.AnimationEventHandler.OnEndAttack -= EndAttack;
                }

                currentUnit = value;

                currentUnit.AnimationEventHandler.OnShootDetect += OnShootDetect;
                currentUnit.AnimationEventHandler.OnBeginAttack += BeginAttack;
                currentUnit.AnimationEventHandler.OnEndAttack += EndAttack;
            }
        }

        protected abstract void OnShootDetect();

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
