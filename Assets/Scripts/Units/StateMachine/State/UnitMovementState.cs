public class UnitMovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;

    private readonly Unit _unit;
    protected Weapon CurrentWeapon;

    public UnitMovementState(IStateSwitcher stateSwitcher, Unit unit)
    {
        StateSwitcher = stateSwitcher;
        _unit = unit;
    }

    protected UnitView UnitView => _unit.View;
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

    protected bool IsMoving() => _unit.Movement.NavMeshAgent.speed == 0;
    protected bool IsAttacking() => _unit.Attack.IsAttacking;
}