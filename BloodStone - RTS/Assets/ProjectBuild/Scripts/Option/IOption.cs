using System.Collections.Generic;

namespace Option
{
    public interface IOption
    {
        IReadOnlyList<DoActionOption> Options { get; }
    }
}