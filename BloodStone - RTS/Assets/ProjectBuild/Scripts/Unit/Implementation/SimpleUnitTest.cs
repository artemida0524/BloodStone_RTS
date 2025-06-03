using Game.Gameplay.Audio;
using Game.Gameplay.Units;
using UnityEngine;

namespace Unit
{
    public class SimpleUnitTest : SimpleUnitBase
    {
        [SerializeField] private UnitAudioHandler audioSourceHandler;
        protected override void Awake()
        {
            base.Awake();
            audioSourceHandler.Init();
        }

    }
}