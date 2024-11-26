public class UnitIdlingState : UnitMovementState
{
    //private const string IsIdling = "IsIdling";

    public UnitIdlingState(IStateSwitcher stateSwitcher, Unit unit) : base(stateSwitcher, unit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //UnitView.StartState(IsIdling);
    }

    public override void Exit()
    {
        base.Exit();

        //UnitView.StopState(IsIdling);
    }

    public override void Update()
    {
        base.Update();

        //if (IsDiying())
        //    StateSwitcher.SwitchState<UnitDiyingState>();

        //if (IsAttacking())
        //{
        //    StateSwitcher.SwitchState<UnitAttackState>();
        //}

        //if (IsMoving())
        //    return;

        //StateSwitcher.SwitchState<UnitRunningState>();
    }
}
