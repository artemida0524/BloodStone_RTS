using System.Collections.Generic;

namespace Option
{
    public interface IOption
    {
        List<DoActionOption> Options { get; }
    }
}