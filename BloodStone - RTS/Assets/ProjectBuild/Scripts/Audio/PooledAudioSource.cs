using Scripts.ObjectPool.Implementation;
using Scripts.ObjectPool.Interface;
using System.Collections;
using UnityEngine;

namespace Game.Gameplay.Audio
{
    public class PooledAudioSource : MonoBehaviour, IPooledObject
    {
        [SerializeField] private PoolObject poolObject;
        [SerializeField] private AudioSource source;

        //public bool IsPlaying => source.isPlaying;

        public IPoolObject PoolObject => poolObject;

        public void PlayOneShot(AudioClip clip)
        {
            source.PlayOneShot(clip);
            StartCoroutine(coroutine(clip.length));
        }


        private IEnumerator coroutine(float length)
        {
            yield return new WaitForSeconds(length);
            poolObject.Push();
        }

    }
}