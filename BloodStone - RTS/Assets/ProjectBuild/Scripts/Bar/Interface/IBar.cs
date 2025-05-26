using Game.Gameplay.Stats;
using System;

namespace Bar
{
    public interface IBar : IStat, IDisposable
    {
        string Name { get; }
    }

}