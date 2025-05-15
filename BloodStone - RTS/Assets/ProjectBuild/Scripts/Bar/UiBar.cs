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
            this.ResourceStat = resourceBar;
            icon.sprite = rowBarAsset.Icon;

            OnDataChange();

            resourceBar.OnDataChange += OnDataChange;
        }

        private void OnDataChange()
        {
            slider.maxValue = ResourceStat.MaxCount;
            slider.value = ResourceStat.Count;


            countText.text = ResourceStat.Count.ToString();
        }

        public override void Dispose()
        {
            ResourceStat.Dispose();
        }
    }

}