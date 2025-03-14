using Unit;

public class InteractableUnit : InteractableBase
{
    public InteractableUnit(UnitBase unitbase) : base(unitbase)
    {
        Actions.Add(new DoActionOption() {Action = unitbase.DoSomething, Name = nameof(unitbase.DoSomething) });
    }
}




