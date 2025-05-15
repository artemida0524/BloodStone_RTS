using System;

namespace Bar
{
    public interface IStats : IDisposable
    {
        string Name { get; }
        int MaxCount { get; }
        int Count { get; }

        event Action OnDataChange;
    }

}