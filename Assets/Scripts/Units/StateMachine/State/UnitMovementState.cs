public class UnitMovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;

    private readonly Unit _unit;

    public UnitMovementState(IStateSwitcher stateSwitcher, Unit unit)
    {
        StateSwitcher = stateSwitcher;
        _unit = unit;
    }

    protected Unit unit => unit;

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
       
    }

    public virtual void Update()
    {
    }
}