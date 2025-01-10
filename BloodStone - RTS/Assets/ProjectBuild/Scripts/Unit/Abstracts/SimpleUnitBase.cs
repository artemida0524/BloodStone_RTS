using Faction;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public abstract class SimpleUnitBase : UnitBase
    {
        protected override StateBehaviourBase InitializeState()
        {
            return new SimpleStateBehaviour(this);
        }
    }
}
