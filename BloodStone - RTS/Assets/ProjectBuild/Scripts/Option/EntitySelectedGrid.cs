using System.Collections.Generic;
using Unit;
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

        public void Init(List<IInteractable> interactables)
        {
            alreadySelect.Clear();

            foreach (var item in Items)
            {
                item.Unselect();
            }

            for (int i = 0; i < interactables.Count; i++)
            {
                Items[i].SetInteractable(interactables[i]);
            }
        }

        public void RemoveAll()
        {
            foreach (var item in Items)
            {
                item.Remove();
            }
        }

        private void OnClickHandler(EntitySelect entitySelect)
        {
            optionsGrid.RemoveAll();

            if (entitySelect.Interactable == null) return;

            if (!alreadySelect.Contains(entitySelect))
            {
                if(!Input.GetKey(KeyCode.LeftShift))
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


            optionsGrid.Init(alreadySelect);


        }
    }
}