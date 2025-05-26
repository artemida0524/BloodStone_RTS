using Game.Gameplay.Entity;
using System.Collections.Generic;

namespace Game.Gameplay.Options
{
    public class OptionBase : IOption
    {
        public IReadOnlyList<DoActionOption> Options => options;
        public EntityInfoSO EntityInfo { get; protected set; }

        protected List<DoActionOption> options = new List<DoActionOption>();

        public OptionBase(EntityBase entityBase)
        {
            this.EntityInfo = entityBase.EntityInfo;
            options.Add(new DoActionOption() { Action = entityBase.Delete, Name = nameof(entityBase.Delete) });
        }
    }
}