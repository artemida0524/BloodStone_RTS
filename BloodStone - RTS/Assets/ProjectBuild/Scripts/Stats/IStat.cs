using System;

namespace Game.Gameplay.Stats
{
	public interface IStat
	{
		string Name { get; }
		int MaxCount { get; }
        int Count { get; }
		event EventHandler OnDataChange;


    }

}