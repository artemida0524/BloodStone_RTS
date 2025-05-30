using State;
using Unit;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Audio
{
    // EXAMPLE
    public class UnitAudioHandler : MonoBehaviour
    {
        [SerializeField] private SimpleUnitTest unit;
        [SerializeField] private AudioClip clip;

        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService service)
        {
            _audioService = service;
        }

        public void Init()
        {
            unit.OnStateChanged += OnStateChangeHandler;
        }

        private void OnStateChangeHandler(StateBase state)
        {
            Play(clip);
        }

        private void Play(AudioClip clip)
        {
            _audioService.PlaySound(clip, unit.transform);
        }
    }
}