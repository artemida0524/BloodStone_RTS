using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class StateMachine
    {
        public StateBase State { get; private set; }

        public StateMachine()
        {
            
        }

        public StateMachine(StateBase enterState)
        {
            ChangeState(enterState);
        }

        public void ChangeState(StateBase state)
        {
            State?.Exit();

            this.State = state;

            State?.Enter();
        }

        public void Update()
        {
            State?.Update();
        }
    }
}
