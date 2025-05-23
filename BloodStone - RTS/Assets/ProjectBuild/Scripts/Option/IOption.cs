using System.Collections.Generic;

namespace BloodStone.Gameplay.Options
{
    public interface IOption
    {
        IReadOnlyList<DoActionOption> Options { get; }
    }
}