using System.Collections.Generic;

namespace Bar
{
    public interface IEntityStats
    {
        IEnumerable<IStats> EntityStats { get; }
    }

}