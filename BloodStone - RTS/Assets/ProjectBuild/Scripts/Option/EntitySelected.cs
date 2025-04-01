using Select;
using System;
using Unit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Option
{
    public class EntitySelected : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private RectTransform selectView;
        private Button button;

        public ISelectable SelectedEntity { get; private set; }
        public event Action<EntitySelected> OnClick;

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

        public void SetEntity(ISelectable select, Sprite sprite)
        {
            this.SelectedEntity = select;
            icon.sprite = sprite;
        }

        public void Remove()
        {
            icon.sprite = null;
            SelectedEntity = null;
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