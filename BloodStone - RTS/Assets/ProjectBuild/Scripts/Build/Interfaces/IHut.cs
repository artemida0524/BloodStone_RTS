using Game.Gameplay.Build;
namespace Game.Gameplay.Build
{

	public interface IHut : IBuild
	{
		int MaxUnitCount { get; }
	} 
}