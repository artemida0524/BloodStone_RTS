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
        [SerializeField] private AudioDataAsset exampleData;

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
            Play(exampleData);
        }

        private void Play(AudioDataAsset exampleData)
        {
            _audioService.PlaySound(exampleData, unit.transform);
        }
    }
}