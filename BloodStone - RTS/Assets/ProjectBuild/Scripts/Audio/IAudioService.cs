using UnityEngine;

namespace Game.Gameplay.Audio
{
    public interface IAudioService
    {
        void Init();
        void PlaySound(AudioClip clip, Vector3 position);
        void PlaySound(AudioClip clip, Transform transform);
    }

}