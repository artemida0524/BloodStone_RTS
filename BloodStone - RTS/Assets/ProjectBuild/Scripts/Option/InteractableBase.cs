using Entity;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Option
{
    public class InteractableBase : IInteractable
    {
        public Sprite Icon { get; protected set; }

        public List<DoActionOption> Actions { get; } = new List<DoActionOption>();

        public InteractableBase(EntityBase entityBase)
        {
            Icon = (entityBase as ISelectable).Sprite; //temporarily
            Actions.Add(new DoActionOption() { Action = entityBase.Delete, Name = nameof(entityBase.Delete) });
        }
    }


}


