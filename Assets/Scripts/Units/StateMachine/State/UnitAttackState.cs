using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackState : UnitMovementState
{
    private const string IsAttackingZombie = "IsAttacking";

    public UnitAttackState(IStateSwitcher stateSwitcher, Unit unit) : base(stateSwitcher, unit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //UniteView.StartState(IsAttackingZombie);
    }

    public override void Exit()
    {
        base.Exit();

        //UnitView.StopState(IsAttackingZombie);
    }

    public override void Update()
    {
        base.Update();

        //if (IsDiying())
        //{
        //    StateSwitcher.SwitchState<UnitDiyingState>();
        //}
        //else if (IsAttacking())
        //    return;

        StateSwitcher.SwitchState<UnitIdlingState>();
    }
}
