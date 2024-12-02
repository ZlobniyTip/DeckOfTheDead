public class UnitRunningState : UnitMovementState
{
    const string IsRunningMelle = "IsRunningMelle";
    const string IsRunningPistol = "IsRunningPistol";
    const string IsRunningRifle = "IsRunningRifle";

    public UnitRunningState(IStateSwitcher stateSwitcher, Unit unit) : base(stateSwitcher, unit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        switch (Unit.Attack.CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                UnitView.StartState(IsRunningMelle);
                break;

            case WeaponType.Pistol:
                UnitView.StartState(IsRunningPistol);
                break;

            case WeaponType.Rifle:
                UnitView.StartState(IsRunningRifle);
                break;
        }

        CurrentWeapon = Unit.Attack.CurrentWeapon;
    }

    public override void Exit()
    {
        base.Exit();

        switch (CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                UnitView.StopState(IsRunningMelle);
                break;

            case WeaponType.Pistol:
                UnitView.StopState(IsRunningPistol);
                break;

            case WeaponType.Rifle:
                UnitView.StopState(IsRunningRifle);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (IsMoving())
            StateSwitcher.SwitchState<UnitIdlingState>();
    }
}