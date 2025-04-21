using System;

namespace Unit
{
    public class AnimationEventCallBackWoker : AnimationEventCallBasckBase
    {
        public event Action OnCall;
        private void CallBack()
        {
            OnCall?.Invoke();
        }
    }

}
