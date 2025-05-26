using System;

namespace Game.Gameplay.Units.Animation
{


    public class AnimationEventCallBackAttack : AnimationEventCallBasckBase
    {
        public event Action OnBeginAttack;
        public event Action OnEndAttack;

        public event Action OnShootDetect;
        public event Action OnCreateBullet;

        
        private void BeginAttackCallBack()
        {
            OnBeginAttack?.Invoke();
        }

        private void EndAttackCallBack()
        {
            OnEndAttack?.Invoke();
        }

        private void ShootAnimationEventCallBack()
        {
            OnShootDetect?.Invoke();
        }

        private void CreateBulletAnimationEventCallBack()
        {
            OnCreateBullet?.Invoke();
        }

    }

}
