using BloodStone.Gameplay.Units.Animation;
using System;

namespace BloodStone.Gameplay.Units
{
    public class AnimationEventCallBackWoker : AnimationEventCallBasckBase
    {
        public event Action OnChopTree;
        private void OnChopTreeCallBack()
        {
            OnChopTree?.Invoke();
        }
    }

}
