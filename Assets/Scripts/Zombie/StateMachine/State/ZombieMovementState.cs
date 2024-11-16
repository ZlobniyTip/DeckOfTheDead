using UnityEngine;

public class ZombieMovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;

    private readonly Enemy _enemy;

    public ZombieMovementState(IStateSwitcher stateSwitcher, Enemy enemy)
    {
        StateSwitcher = stateSwitcher;
        _enemy = enemy;
    }

    protected ZombieView ZombieView => _enemy.ZombieView;
    protected Enemy Enemy => _enemy;

    public virtual void Enter()
    {
        Debug.Log(GetType());
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }

    protected bool IsMoving() => Enemy.Movement.NavMeshAgent.speed == 0;
    protected bool IsAttacking() => Enemy.ZombieAttack.IsAttacking;
}