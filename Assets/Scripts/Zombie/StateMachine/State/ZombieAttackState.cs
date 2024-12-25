

using UnityEngine;

public class ZombieAttackState : ZombieMovementState
{
    private const string IsAttackingZombie = "IsAttacking";

    public ZombieAttackState(IStateSwitcher stateSwitcher, Zombie enemy) : base(stateSwitcher, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ZombieView.StartState(IsAttackingZombie);
    }

    public override void Exit()
    {
        base.Exit();

        ZombieView.StopState(IsAttackingZombie);
    }

    public override void Update()
    {
        base.Update();

        if (IsDiying())
        {
            StateSwitcher.SwitchState<ZombieDiyingState>();
        }

        if (IsAttacking())
        {
            return;
        }
        else 
        {
            StateSwitcher.SwitchState<ZombieRunningState>();
        }
    }
}