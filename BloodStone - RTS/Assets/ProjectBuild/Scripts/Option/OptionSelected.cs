using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BloodStone.Gameplay.Options
{
    public class OptionSelected : MonoBehaviour
    {
        [SerializeField] private Image image;
        private Button button;

        public List<DoActionOption> OptionList { get; private set; } = new List<DoActionOption>();
        public event Action<OptionSelected> OnClick;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClickHandler);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClickHandler);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            OnClick?.Invoke(this);
        }

        public void SetOptions(List<DoActionOption> options, Sprite sprite)
        {
            this.OptionList = options;
            this.image.sprite = sprite;
        }

        public void Remove()
        {
            image.sprite = null;
            OptionList.Clear();
        }

    }
}
