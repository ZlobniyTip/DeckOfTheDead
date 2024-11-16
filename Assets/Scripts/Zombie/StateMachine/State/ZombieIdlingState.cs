public class ZombieIdlingState : ZombieMovementState
{
    private const string IsIdling = "IsIdling";

    public ZombieIdlingState(IStateSwitcher stateSwitcher, Enemy enemy) : base(stateSwitcher, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ZombieView.StartState(IsIdling);
    }

    public override void Exit()
    {
        base.Exit();

        ZombieView.StopState(IsIdling);
    }

    public override void Update()
    {
        base.Update();

        if (IsAttacking())
        {
            StateSwitcher.SwitchState<ZombieAttackState>();
        }

        if (IsMoving())
            return;

        StateSwitcher.SwitchState<ZombieRunningState>();
    }
}
