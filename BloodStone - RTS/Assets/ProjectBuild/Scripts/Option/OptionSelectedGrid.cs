using static Game.Gameplay.Options.OptionUnitBase.Option;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Options
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

            var newDict = dict.Where((a) =>
            {
                foreach (var item in a.Value)
                {
                    if(item.myEnum == ActionType.More && a.Value.Count > 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            });

            int indexer = 0;
            foreach (var item in newDict)
            {
                if (item.Value.Count > countOptioin)
                {
                    Debug.LogWarning($"Too much Action in one _entity with the same name: {item.Key}");
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