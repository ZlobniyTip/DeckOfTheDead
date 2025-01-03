public class ZombieRunningState : ZombieMovementState
{
    private const string IsRunning = "IsRunning";

    public ZombieRunningState(IStateSwitcher stateSwitcher, Zombie enemy) : base(stateSwitcher, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ZombieView.StartState(IsRunning);
    }

    public override void Exit()
    {
        base.Exit();

        ZombieView.StopState(IsRunning);
    }

    public override void Update()
    {
        base.Update();

        if (IsAttacking())
        {
            StateSwitcher.SwitchState<ZombieAttackState>();
            return;
        }

        if (IsDiying())
            StateSwitcher.SwitchState<ZombieDiyingState>();

    }
}