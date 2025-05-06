using System;

namespace Unit
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
