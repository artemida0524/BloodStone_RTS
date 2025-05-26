using System.Collections.Generic;

namespace Bar
{
    public interface IEntityStats
    {
        IEnumerable<IBar> EntityStats { get; }
    }

}