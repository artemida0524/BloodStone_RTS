using UnityEngine;

namespace Game.Gameplay.Audio
{
    [CreateAssetMenu(fileName = "AudioDataAsset", menuName = "Audio/AudioDataAsset")]
    public class AudioDataAsset : ScriptableObject
    {
        [field: SerializeField] public AudioData Data { get; private set; }
    }

}