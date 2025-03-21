using System.Collections.Generic;
using UnityEngine;

namespace Option
{
    public interface IOption
    {
        public EntityInfoSO EntityInfo { get; }
        List<DoActionOption> Options { get; }
    }
}