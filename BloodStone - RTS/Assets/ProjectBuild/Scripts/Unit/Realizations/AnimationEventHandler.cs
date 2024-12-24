using System;
using UnityEngine;

namespace Unit
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnShootDetect;
        public event Action OnCreateBullet;

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
