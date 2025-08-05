using System.Collections.Generic;

namespace Game.Gameplay.Options
{
    public interface IOption
    {
        IReadOnlyList<DoActionOption> Options { get; }
    }
}