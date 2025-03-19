using System.Collections;
using System.Collections.Generic;
using Unit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Option
{
    public class OptionSelected : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private OptionSOList options;
        private Button button;

        public bool CanInteraction = false;


        private List<DoActionOption> optionList = new();

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }


        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (CanInteraction)
            {
                Debug.Log("wefwef");
                foreach (var item in optionList)
                {
                    item.Action?.Invoke();
                } 
            }
        }

        public void SetActions(List<DoActionOption> options)
        {
            this.optionList = options;
            DoActionOption option = options[0];

            foreach (var item in this.options.Options)
            {
                if (option.Name == item.Name)
                {
                    image.sprite = item.Icon;
                    return;
                }
            }
            image.sprite = null;
        }


        public void Remove()
        {
            image.sprite = null;
            //optionList.Clear();
            CanInteraction = false;
        }

    } 
}
