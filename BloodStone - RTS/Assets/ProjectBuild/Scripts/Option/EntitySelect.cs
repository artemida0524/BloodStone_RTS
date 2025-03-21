using System;
using UnityEngine;
using UnityEngine.UI;

namespace Option
{
    public class EntitySelect : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private RectTransform selectView;
        private Button button;

        public IOption Option { get; private set; }
        public event Action<EntitySelect> OnClick;

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

        public void SetEntity(IOption entity, Sprite sprite)
        {
            this.Option = entity;
            icon.sprite = sprite;
        }

        public void Remove()
        {
            icon.sprite = null;
            Option = null;
        }

        public void Select()
        {
            selectView.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            selectView.gameObject.SetActive(false);
        }
    }
}