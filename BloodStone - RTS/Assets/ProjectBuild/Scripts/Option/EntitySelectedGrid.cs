using System.Collections.Generic;
using UnityEngine;

namespace Option
{
    public class EntitySelectedGrid : MonoBehaviour
    {
        [field: SerializeField] public List<EntitySelect> Items { get; private set; }
        [SerializeField] private OptionSelectedGrid optionsGrid;

        private List<EntitySelect> alreadySelect = new List<EntitySelect>();

        private void Start()
        {
            foreach (var item in Items)
            {
                item.OnClick += OnClickHandler;
            }
        }

        public void Init(List<IOption> options)
        {
            alreadySelect.Clear();

            foreach (var item in Items)
            {
                item.Unselect();
            }

            for (int i = 0; i < options.Count; i++)
            {
                Items[i].SetEntity(options[i], options[i].EntityInfo.Icon);
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

        private void OnClickHandler(EntitySelect entitySelect)
        {
            optionsGrid.RemoveAll();

            if (entitySelect.Option == null) return;

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
            List<IOption> interactables = new List<IOption>();
            foreach (var item in alreadySelect)
            {
                interactables.Add(item.Option);
            }

            return interactables;
        }

    }
}