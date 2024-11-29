public class UnitIdlingState : UnitMovementState
{
    private const string IsIdling = "IsIdling";

    public UnitIdlingState(IStateSwitcher stateSwitcher, Unit unit) : base(stateSwitcher, unit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UnitView.StartState(IsIdling);
    }

    public override void Exit()
    {
        base.Exit();

        UnitView.StopState(IsIdling);
    }

    public override void Update()
    {
        base.Update();

        if (IsMoving())
            return;

        StateSwitcher.SwitchState<UnitRunningState>();
    }
}
