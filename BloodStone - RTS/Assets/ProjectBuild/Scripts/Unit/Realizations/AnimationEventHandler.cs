using System;
using UnityEngine;

namespace Unit
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnBeginAttack;
        public event Action OnEndAttack;

        public event Action OnShootDetect;
        public event Action OnCreateBullet;

        
        private void BeginAttackCallBack()
        {
            OnBeginAttack?.Invoke();
        }

        private void OnEndAttackCallBack()
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
