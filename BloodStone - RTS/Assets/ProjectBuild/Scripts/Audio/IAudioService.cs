using UnityEngine;

namespace Game.Gameplay.Audio
{

    public interface IAudioService
    {
        void Init();

        void PlaySound(AudioData data, Vector3 position);
        void PlaySound(AudioData data, Transform transform);

        void PlaySound(AudioDataAsset data, Vector3 position);
        void PlaySound(AudioDataAsset data, Transform transform);
    }

}