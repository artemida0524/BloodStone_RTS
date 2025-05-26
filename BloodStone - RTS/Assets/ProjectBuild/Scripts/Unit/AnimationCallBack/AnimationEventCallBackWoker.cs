using Game.Gameplay.Units.Animation;
using System;

namespace Game.Gameplay.Units
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
