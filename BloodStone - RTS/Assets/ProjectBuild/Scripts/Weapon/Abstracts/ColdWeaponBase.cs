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
                    currentUnit.AnimationEventCallBalck.OnBeginAttack -= BeginAttack;
                    currentUnit.AnimationEventCallBalck.OnEndAttack -= EndAttack;
                }

                currentUnit = value;

                currentUnit.AnimationEventCallBalck.OnShootDetect += OnShootDetect;
                currentUnit.AnimationEventCallBalck.OnBeginAttack += BeginAttack;
                currentUnit.AnimationEventCallBalck.OnEndAttack += EndAttack;
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
