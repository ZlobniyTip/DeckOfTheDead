public class UnitRunningState : UnitMovementState
{
    private const string IsRunning = "IsRunning";

    public UnitRunningState(IStateSwitcher stateSwitcher, Unit unit) : base(stateSwitcher, unit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UnitView.StartState(IsRunning);
    }

    public override void Exit()
    {
        base.Exit();

        UnitView.StopState(IsRunning);
    }

    public override void Update()
    {
        base.Update();

        if (IsMoving())
            StateSwitcher.SwitchState<UnitIdlingState>();
    }
}
