using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public abstract class StateBase
    {
        public bool IsFinished { get; protected set; } = false;

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }

    }

}