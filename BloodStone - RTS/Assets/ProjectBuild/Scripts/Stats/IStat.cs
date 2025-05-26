using System;

namespace Game.Gameplay.Stats
{
	public interface IStat
	{
		int MaxCount { get; }
        int Count { get; }
		event EventHandler OnDataChange;
    }

}