using System;

namespace Unit
{
    public interface IHealth
    {
        int MaxCountHealth { get; }
        int CountHealth { get; }
        event Action<int> OnHealthChange;
    }


}

