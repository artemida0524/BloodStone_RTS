using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public abstract class StateBehaviourBase
    {
        public StateMachine StateMachine { get; protected set; }

        public abstract void Interaction(Collider collision);
        public abstract void SetState(StateBase state);
        public abstract void SetStateIfFinished();

    }
}
