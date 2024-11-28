using UnityEngine;

public class ZombieDiyingState : ZombieMovementState
{
    private const string IsDiyingVar1 = "IsDiyingVar1";
    private const string IsDiyingVar2 = "IsDiyingVar2";

    private int _randomState;

    public ZombieDiyingState(IStateSwitcher stateSwitcher, Enemy enemy) : base(stateSwitcher, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _randomState = Random.Range(0, 2);

        if (_randomState == 0)
        {
            ZombieView.StartState(IsDiyingVar1);
        }
        else
        {
            ZombieView.StartState(IsDiyingVar2);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (_randomState == 0)
        {
            ZombieView.StopState(IsDiyingVar1);
        }
        else
        {
            ZombieView.StopState(IsDiyingVar2);
        }
    }

    public override void Update()
    {
        base.Update();
    }
}