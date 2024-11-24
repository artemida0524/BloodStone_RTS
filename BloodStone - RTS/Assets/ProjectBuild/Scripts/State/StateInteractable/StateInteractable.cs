using UnityEngine;

namespace State
{
    [RequireComponent(typeof(CapsuleCollider))]
	public class StateInteractable : MonoBehaviour
	{
        public StateBehaviourBase Behaviour { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            Behaviour.Interaction(other);
        }

        private void Update()
        {
            if (Behaviour.StateMachine.State.IsFinished)
            {
                Behaviour.SetStateIfFinished(); 
            }
        }

        public void SetState(StateBase state)
        {
            Behaviour.SetState(state);

        }




        public void UpdateState()
        {
            Behaviour.StateMachine?.Update();
        }

    }
}
