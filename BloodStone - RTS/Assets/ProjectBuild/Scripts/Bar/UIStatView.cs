using Game.Gameplay.Stats;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class UIStatView : UIStatsViewBase
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image background;
        [SerializeField] private Image row;

        public override void Init(IStat stat, UIBarDataAsset rowBarAsset)
        {
            this.Stat = stat;

            OnDataChange(this, EventArgs.Empty);

            background.sprite = rowBarAsset.Background;
            row.sprite = rowBarAsset.Row;
            
            Stat.OnDataChange += OnDataChange;
        }

        private void OnDataChange(object sender, EventArgs args)
        {
            slider.maxValue = Stat.MaxCount;
            slider.value = Stat.Count;
        }

        public override void Dispose()
        {
            Stat.OnDataChange -= OnDataChange;
        }
    }

}