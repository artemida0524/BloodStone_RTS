using Scripts.ObjectPool.Provider;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Audio
{

    public class AudioService : MonoBehaviour, IAudioService
    {

        private PoolProviderTest _poolProvider;

        [Inject]
        private void Construct(PoolProviderTest poolProvider)
        {
            _poolProvider = poolProvider;
        }






        public void Init()
        {

        }

        public void PlaySound(AudioClip clip, Vector3 position)
        {
            PooledAudioSource source = Play(clip);
            source.transform.position = position;
        }

        public void PlaySound(AudioClip clip, Transform transform)
        {
            PooledAudioSource source = Play(clip);

            source.transform.SetParent(transform);
            source.transform.localPosition = Vector3.zero;
        }

        private PooledAudioSource Play(AudioClip clip)
        {
            PooledAudioSource source = _poolProvider.Pull(PoolsNames.AUDIO_SOURCE).GetOwner<PooledAudioSource>();
            source.gameObject.SetActive(true);
            source.PlayOneShot(clip);

            return source;
        }
    }
}