public class AttackState : MovementState
{
    const string IsAttackingMelle = "IsMelleAttack";
    const string IsShootingPistol = "IsShootingPistol";
    const string IsShootingRifle = "IsShootingRifle";
    const string IsShootinShotgun = "IsShootinShotgun";
    const string IsShootingHunterRifle = "IsShootingHunterRifle";

    public AttackState(IStateSwitcher stateSwitcher, Character character) : base(stateSwitcher, character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        switch (Character.CharacterShooting.CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                CharacterView.StartState(IsAttackingMelle);
                break;

            case WeaponType.Pistol:
                CharacterView.StartState(IsShootingPistol);
                break;

            case WeaponType.Rifle:
                CharacterView.StartState(IsShootingRifle);
                break;

            case WeaponType.Shotgun:
                CharacterView.StartState(IsShootinShotgun);
                break;

            case WeaponType.HunterRifle:
                CharacterView.StartState(IsShootingHunterRifle);
                break;

            case WeaponType.FlameThrower:
                CharacterView.StartState(IsShootingHunterRifle);
                break;
        }

        CurrentWeapon = Character.CharacterShooting.CurrentWeapon;
    }

    public override void Exit()
    {
        base.Exit();

        switch (CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                CharacterView.StopState(IsAttackingMelle);
                break;

            case WeaponType.Pistol:
                CharacterView.StopState(IsShootingPistol);
                break;

            case WeaponType.Rifle:
                CharacterView.StopState(IsShootingRifle);
                break;

            case WeaponType.Shotgun:
                CharacterView.StopState(IsShootinShotgun);
                break;

            case WeaponType.HunterRifle:
                CharacterView.StopState(IsShootingHunterRifle);
                break;

            case WeaponType.FlameThrower:
                CharacterView.StopState(IsShootingHunterRifle);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (IsAttacking())
            return;

        StateSwitcher.SwitchState<IdlingState>();
    }
}