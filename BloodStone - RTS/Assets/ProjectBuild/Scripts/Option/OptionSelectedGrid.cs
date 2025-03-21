using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace Option
{
    public class OptionSelectedGrid : MonoBehaviour
    {
        [field: SerializeField] public List<OptionSelected> Items { get; private set; }
        [SerializeField] private OptionSOList optionSOList;

        private Dictionary<string, List<DoActionOption>> dict = new Dictionary<string, List<DoActionOption>>();


        private void Start()
        {
            foreach (var item in Items)
            {
                item.OnClick += OnClickHandler;
            }
        }

        public void Init(List<IOption> interactions)
        {
            dict.Clear();
            RemoveAll();

            int countOptioin = interactions.Count;

            foreach (var item in interactions)
            {
                foreach (var option in item.Options)
                {
                    if (!dict.ContainsKey(option.Name))
                    {
                        dict[option.Name] = new List<DoActionOption>();
                    }
                    dict[option.Name].Add(option);
                }
            }
            int indexer = 0;
            foreach (var item in dict)
            {
                if (item.Value.Count > countOptioin)
                {
                    Debug.LogWarning($"Too much Action in one entity with the same name: {item.Key}");
                    return;
                }
                if (item.Value.Count == countOptioin)
                {
                    DoActionOption option = item.Value[0];

                    foreach (var item2 in this.optionSOList.Options)
                    {
                        if (option.Name == item2.Name)
                        {
                            Items[indexer].SetOptions(item.Value, item2.Icon);
                            indexer++;
                            break;
                        }
                    }
                }
            }
        }

        public void RemoveAll()
        {
            foreach (var item in Items)
            {
                item.Remove();
            }
        }
        private void OnClickHandler(OptionSelected option)
        {
            List<DoActionOption> options = new List<DoActionOption>(option.OptionList);

            foreach (var item in options)
            {
                item.Action?.Invoke();
            }
        }
    }
}




