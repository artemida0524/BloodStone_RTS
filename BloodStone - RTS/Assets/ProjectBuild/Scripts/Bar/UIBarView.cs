using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class UIBarView : UIBarViewBase
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image background;
        [SerializeField] private Image row;

        public override void Init(IStats stats, UIBarDataAsset rowBarAsset)
        {
            this.Stat = stats;

            OnDataChange();

            background.sprite = rowBarAsset.Background;
            row.sprite = rowBarAsset.Row;
            
            Stat.OnDataChange += OnDataChange;
        }

        private void OnDataChange()
        {
            slider.maxValue = Stat.MaxCount;
            slider.value = Stat.Count;
        }

        public override void Dispose()
        {
            Stat.Dispose();
        }
    }

}