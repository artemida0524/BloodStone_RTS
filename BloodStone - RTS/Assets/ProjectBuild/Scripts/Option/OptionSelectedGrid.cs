using System.Collections.Generic;
using Unit;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Option
{
    public class OptionSelectedGrid : MonoBehaviour
    {
        [field: SerializeField] public List<OptionSelected> Items { get; private set; }

        private Dictionary<string, List<DoActionOption>> dict = new Dictionary<string, List<DoActionOption>>();

        public void Init(List<DoActionOption> interactables)
        {
            for (int i = 0; i < interactables.Count; i++)
            {
                Items[i].SetAction(interactables[i]);
            }
        }


        public void Init(List<EntitySelect> entitySelects)
        {
            dict.Clear();
            int countOptioin = entitySelects.Count;

            foreach (var item in entitySelects)
            {
                foreach (var option in item.Interactable.Actions)
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
                if(item.Value.Count == countOptioin)
                {
                    Items[indexer].SetActions(item.Value);
                    indexer++;
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
    }
}




