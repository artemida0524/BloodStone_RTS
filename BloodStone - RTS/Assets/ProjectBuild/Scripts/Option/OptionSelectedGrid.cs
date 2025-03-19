using System.Collections.Generic;
using Unit;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Option
{
    public class OptionSelectedGrid : MonoBehaviour
    {
        [field: SerializeField] public List<OptionSelected> Items { get; private set; }

        private Dictionary<string, List<DoActionOption>> dict = new Dictionary<string, List<DoActionOption>>();


        public void Init(List<IInteractable> interactions)
        {
            dict.Clear();

            //RemoveAll();

            int countOptioin = interactions.Count;

            foreach (var item in interactions)
            {
                foreach (var option in item.Actions)
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
                    Items[indexer].CanInteraction = true;
                    indexer++;
                }
            }


            for (int i = indexer; i < Items.Count; i++)
            {
                Items[i].CanInteraction = false;
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




