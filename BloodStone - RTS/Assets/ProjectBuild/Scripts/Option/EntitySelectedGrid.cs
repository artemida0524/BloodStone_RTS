using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Option
{
    public class EntitySelectedGrid : MonoBehaviour
    {
        [field: SerializeField] public List<EntitySelected> Items { get; private set; }
        [SerializeField] private OptionSelectedGrid optionsGrid;

        private List<EntitySelected> alreadySelect = new List<EntitySelected>();

        private void Start()
        {
            foreach (var item in Items)
            {
                item.OnClick += OnClickHandler;
            }
        }

        public void Init(IReadOnlyList<ISelectable> selectables)
        {
            alreadySelect.Clear();

            foreach (var item in Items)
            {
                item.Unselect();
            }

            for (int i = 0; i < selectables.Count; i++)
            {
                Items[i].SetEntity(selectables[i], selectables[i].EntityInfo.Icon);
            }

            if (selectables.Count == 1)
            {
                OnClickHandler(Items[0]);
            }

        }

        public void RemoveAll()
        {
            foreach (var item in Items)
            {
                item.Remove();
                item.Unselect();
            }
        }

        private void OnClickHandler(EntitySelected entitySelect)
        {
            if (entitySelect.SelectedEntity == null) return;
            optionsGrid.RemoveAll();
    

            if (!alreadySelect.Contains(entitySelect))
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    foreach (var item in alreadySelect)
                    {
                        item.Unselect();
                    }
                    alreadySelect.Clear();
                }
                alreadySelect.Add(entitySelect);
                entitySelect.Select();
            }
            else
            {
                entitySelect.Unselect();
                alreadySelect.Remove(entitySelect);
            }
            optionsGrid.Init(GetInteractables());
        }

        private List<IOption> GetInteractables()
        {
            List<IOption> optins = new List<IOption>();
            foreach (var item in alreadySelect)
            {
                optins.Add(item.SelectedEntity.Options);
            }

            return optins;
        }

    }
}