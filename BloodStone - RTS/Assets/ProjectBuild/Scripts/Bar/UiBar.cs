using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class UIBar : UIBarViewBase, IDisposable
    {
        [SerializeField] private Image icon;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI countText;

        public override void Init(IStats resourceBar, UIBarDataAsset rowBarAsset)
        {
            this.Stat = resourceBar;
            icon.sprite = rowBarAsset.Icon;

            OnDataChange();

            resourceBar.OnDataChange += OnDataChange;
        }

        private void OnDataChange()
        {
            slider.maxValue = Stat.MaxCount;
            slider.value = Stat.Count;


            countText.text = Stat.Count.ToString();
        }

        public override void Dispose()
        {
            Stat.Dispose();
        }
    }

}