using Build;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;
using Zenject;

public class FactionInjection : MonoInstaller
{
    [SerializeField] private Build.Faction faction;
    public override void InstallBindings()
    {
        Container
            .BindInstance(faction)
            .AsSingle();
    }
}
