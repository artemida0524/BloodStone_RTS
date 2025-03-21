using Entity;
using System.Collections.Generic;
using UnityEngine;

namespace Option
{
    public class OptionBase : IOption
    {
        public List<DoActionOption> Options { get; } = new List<DoActionOption>();

        public EntityInfoSO EntityInfo { get; protected set; }

        public OptionBase(EntityBase entityBase)
        {
            this.EntityInfo = entityBase.EntityInfo;
            Options.Add(new DoActionOption() { Action = entityBase.Delete, Name = nameof(entityBase.Delete) });
        }
    }
}