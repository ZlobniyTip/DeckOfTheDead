using UnityEngine;

public class ZombieDiyingState : ZombieMovementState
{
    private const string IsDiyingVar1 = "IsDiyingVar1";
    private const string IsDiyingVar2 = "IsDiyingVar2";

    public ZombieDiyingState(IStateSwitcher stateSwitcher, Enemy enemy) : base(stateSwitcher, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        int random = Random.Range(0, 2);

        if (random == 0)
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

        int random = Random.Range(0, 2);

        if (random == 0)
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
        Debug.Log(GetType());
    }
}