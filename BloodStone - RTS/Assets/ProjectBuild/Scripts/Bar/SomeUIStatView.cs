using Game.Gameplay.Stats;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class SomeUIStatView : UIStatsViewBase, IDisposable
    {
        [SerializeField] private Image icon;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI countText;

        public override void Init(IStat resourceBar, UIBarDataAsset rowBarAsset)
        {
            this.Stat = resourceBar;
            icon.sprite = rowBarAsset.Icon;

            OnDataChange(this, EventArgs.Empty);

            resourceBar.OnDataChange += OnDataChange;
        }

        private void OnDataChange(object sender, EventArgs args)
        {
            slider.maxValue = Stat.MaxCount;
            slider.value = Stat.Count;


            countText.text = Stat.Count.ToString();
        }

        public override void Dispose()
        {
            Stat.OnDataChange -= OnDataChange;
        }
    }

}