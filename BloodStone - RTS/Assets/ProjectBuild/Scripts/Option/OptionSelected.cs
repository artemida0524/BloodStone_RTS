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
        private DoActionOption option;

        private List<DoActionOption> optionList;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            Debug.Log("enable");
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
            //option.Action?.Invoke();

            foreach (var item in optionList)
            {
                item.Action?.Invoke();
            }

        }

        public void SetAction(DoActionOption option)
        {
            this.option = option;

            foreach (var item in options.Options)
            {
                if (this.option.Name == item.Name)
                {
                    image.sprite = item.Icon;
                    return;
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

        }


        public void Remove()
        {
            image.sprite = null;
            option.Name = string.Empty;
            option.Action = null;
        }

    } 
}
