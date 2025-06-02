using Scripts.ObjectPool.Implementation;
using Scripts.ObjectPool.Interface;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Gameplay.Audio
{
    public class PooledAudioSource : MonoBehaviour, IPooledObject
    {
        [SerializeField] private PoolObject poolObject;
        [SerializeField] private AudioSource source;

        
        [field: SerializeField] public AudioGroupType GroupType { get; protected set; } = AudioGroupType.SFX;

        public IPoolObject PoolObject => poolObject;

        public void SetAudioGroup(AudioGroupType type, AudioMixerGroup group)
        {
            GroupType = type;
            source.outputAudioMixerGroup = group;
        }

        public void SetVolume(float range)
        {
            source.volume = range;
        }

        public void PlayOneShot(AudioClip clip)
        {
            source.PlayOneShot(clip);
            StartCoroutine(ReturnToPoolAfterDelay(clip.length));
        }


        private IEnumerator ReturnToPoolAfterDelay(float length)
        {
            yield return new WaitForSeconds(length);
            poolObject.Push();
        }

    }
}