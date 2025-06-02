using UnityEngine;
using Zenject;

namespace Game.Gameplay.Audio
{

    public class AudioTest : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService service)
        {

            _audioService = service;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //_audioService.PlaySound(clip, Vector3.one);
            }
        }
    }
}