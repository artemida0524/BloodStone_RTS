using UnityEngine;

namespace State
{
    [RequireComponent(typeof(CapsuleCollider))]
	public class InteractableUnits : MonoBehaviour
	{
        public StateBehaviourBase Behaviour { get; set; }
        [field: SerializeField] public float radius { get; private set; } = 5f;

        private void OnTriggerEnter(Collider other)
        {
            Behaviour.Interaction(other);
        }

        private void OnValidate()
        {
            GetComponent<CapsuleCollider>().radius = radius;
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
