using Scripts.ObjectPool.Implementation;
using Scripts.ObjectPool.Provider;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Audio
{

    public class AudioService : MonoBehaviour, IAudioService
    {
        private PoolProviderTest _poolProvider;

        private PoolObjectAudio _poolObjectAudio;

        [Inject]
        private void Construct(PoolProviderTest poolProvider)
        {
            _poolProvider = poolProvider;
        }

        public void Init()
        {
            _poolObjectAudio = _poolProvider.GetPullByKey(PoolsNames.AUDIO_SOURCE) as PoolObjectAudio;
        }

        public void PlaySound(AudioData data, Vector3 position)
        {
            PooledAudioSource source = Play(data);
            source.transform.position = position;
        }

        public void PlaySound(AudioData data, Transform transform)
        {

            PooledAudioSource source = Play(data);
            source.transform.SetParent(transform);
            source.transform.localPosition = Vector3.zero;
        }

        public void PlaySound(AudioDataAsset data, Vector3 position)
        {
            PooledAudioSource source = Play(data.Data);
            source.transform.position = position;
        }

        public void PlaySound(AudioDataAsset data, Transform transform)
        {
            PooledAudioSource source = Play(data.Data);
            source.transform.SetParent(transform);
            source.transform.localPosition = Vector3.zero;
        }

        private PooledAudioSource Play(AudioData data)
        {
            PooledAudioSource source = _poolObjectAudio.Pull(data.GroupType).GetOwner<PooledAudioSource>();
            source.gameObject.SetActive(true);
            source.PlayOneShot(data.Clip);
            source.SetVolume(data.Volume);

            return source;
        }

    }
}