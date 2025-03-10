using State;
using System;
using UnityEngine;

namespace State
{
    [RequireComponent(typeof(CapsuleCollider))]
	public class InteractableUnits1234 : MonoBehaviour
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


[Serializable]
public class InteractableUnits
{

    public StateBehaviourBase Behaviour { get; set; }
    [field: SerializeField] public float radius { get; private set; } = 5f;


    public void Update()
    {
        UpdateState();
        if (Behaviour.StateMachine.State.IsFinished)
        {
            Behaviour.SetStateIfFinished();
        }
    }

    public void SetState(StateBase state)
    {
        Behaviour.SetState(state);
    }

    private void UpdateState()
    {
        Behaviour.StateMachine?.Update();
    }


}