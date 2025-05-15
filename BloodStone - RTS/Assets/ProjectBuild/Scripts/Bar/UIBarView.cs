using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class UIBarView : UIBarViewBase
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image rowImage;

        public override void Init(IStats resourceBar, UIBarDataAsset rowBarAsset)
        {
            this.ResourceStat = resourceBar;

            OnDataChange();
            rowImage.color = rowBarAsset.RowColor;
            resourceBar.OnDataChange += OnDataChange;
        }

        private void OnDataChange()
        {
            slider.maxValue = ResourceStat.MaxCount;
            slider.value = ResourceStat.Count;
        }

        public override void Dispose()
        {
            ResourceStat.Dispose();
        }
    }

}