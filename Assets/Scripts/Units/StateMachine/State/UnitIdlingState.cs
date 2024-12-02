public class UnitIdlingState : UnitMovementState
{
    const string IsIdlingMelle = "IsIdlingMelle";
    const string IsIdlingPistol = "IsIdlingPistol";
    const string IsIdlingRifle = "IsIdlingRifle";

    public UnitIdlingState(IStateSwitcher stateSwitcher, Unit unit) : base(stateSwitcher, unit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        switch (unit.Attack.CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                UnitView.StartState(IsIdlingMelle);
                break;

            case WeaponType.Pistol:
                UnitView.StartState(IsIdlingPistol);
                break;

            case WeaponType.Rifle:
                UnitView.StartState(IsIdlingRifle);
                break;
        }

        CurrentWeapon = unit.Attack.CurrentWeapon;
    }

    public override void Exit()
    {
        base.Exit();

        switch (CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                UnitView.StopState(IsIdlingMelle);
                break;

            case WeaponType.Pistol:
                UnitView.StopState(IsIdlingPistol);
                break;

            case WeaponType.Rifle:
                UnitView.StopState(IsIdlingRifle);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (IsAttacking())
        {
            StateSwitcher.SwitchState<UnitAttackState>();
        }

        if (IsMoving())
            return;

        StateSwitcher.SwitchState<UnitRunningState>();
    }
}
