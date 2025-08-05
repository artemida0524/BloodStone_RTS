using System;
using UnityEngine;

namespace Game.Gameplay.Audio
{
    [Serializable]
    public struct AudioData
    {
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public AudioGroupType GroupType { get; private set; }
        [field: SerializeField, Range(0, 1)] public float Volume { get; private set; }
    }
}