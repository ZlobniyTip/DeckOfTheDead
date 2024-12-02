public class UnitAttackState : UnitMovementState
{
    const string IsAttackingMelle = "IsMelleAttack";
    const string IsShootingPistol = "IsShootingPistol";
    const string IsShootingRifle = "IsShootingRifle";
    const string IsShootinShotgun = "IsShootinShotgun";
    const string IsShootingHunterRifle = "IsShootingHunterRifle";

    public UnitAttackState(IStateSwitcher stateSwitcher, Unit unit) : base(stateSwitcher, unit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        switch (unit.Attack.CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                UnitView.StartState(IsAttackingMelle);
                break;

            case WeaponType.Pistol:
                UnitView.StartState(IsShootingPistol);
                break;

            case WeaponType.Rifle:
                UnitView.StartState(IsShootingRifle);
                break;

            case WeaponType.Shotgun:
                UnitView.StartState(IsShootinShotgun);
                break;

            case WeaponType.HunterRifle:
                UnitView.StartState(IsShootingHunterRifle);
                break;

            case WeaponType.FlameThrower:
                UnitView.StartState(IsShootingHunterRifle);
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
                UnitView.StopState(IsAttackingMelle);
                break;

            case WeaponType.Pistol:
                UnitView.StopState(IsShootingPistol);
                break;

            case WeaponType.Rifle:
                UnitView.StopState(IsShootingRifle);
                break;

            case WeaponType.Shotgun:
                UnitView.StopState(IsShootinShotgun);
                break;

            case WeaponType.HunterRifle:
                UnitView.StopState(IsShootingHunterRifle);
                break;

            case WeaponType.FlameThrower:
                UnitView.StopState(IsShootingHunterRifle);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (IsAttacking())
            return;

        StateSwitcher.SwitchState<UnitIdlingState>();
    }
}
