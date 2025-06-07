using Game.Gameplay.Build;
using State;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Hut : BuildInteractableBase, IHut
{
    [field: SerializeField] public int MaxUnitCount { get; protected set; }


    

    public override void Build(BuildType type)
    {
        base.Build(type);

        BuildUtility.OnBuildWasBuiltInvoke(this);

    }

}
