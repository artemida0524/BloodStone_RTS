using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class UIBar : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image icon;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI countText;

        public IBar ResourceBar { get; private set; }


        public void Init(IBar resourceBar, UIBarDataSO rowBarAsset)
        {
            this.ResourceBar = resourceBar;
            icon.sprite = rowBarAsset.Icon;


            OnDataChange();


            resourceBar.OnDataChange = OnDataChange;
        }



        private void OnDataChange()
        {
            slider.maxValue = ResourceBar.MaxCount;
            slider.value = ResourceBar.Count;


            countText.text = ResourceBar.Count.ToString();
        }

        public void Dispose()
        {
            ResourceBar.Dispose();
        }
    } 
}