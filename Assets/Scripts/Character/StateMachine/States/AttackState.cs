public class AttackState : MovementState
{
    const string IsAttackingMelle = "IsMelleAttack";
    const string IsShootingPistol = "IsShootingPistol";
    const string IsShootingRifle = "IsShootingRifle";

    public AttackState(IStateSwitcher stateSwitcher, Character character) : base(stateSwitcher, character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        switch (Character.CharacterShooting.CurrentWeapon.WeaponStatus)
        {
            case WeaponStatus.Melle:
                CharacterView.StartState(IsAttackingMelle);
                break;

            case WeaponStatus.Pistol:
                CharacterView.StartState(IsShootingPistol);
                break;

            case WeaponStatus.Rifle:
                CharacterView.StartState(IsShootingRifle);
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();

        switch (Character.CharacterShooting.CurrentWeapon.WeaponStatus)
        {
            case WeaponStatus.Melle:
                CharacterView.StopState(IsAttackingMelle);
                break;

            case WeaponStatus.Pistol:
                CharacterView.StopState(IsShootingPistol);
                break;

            case WeaponStatus.Rifle:
                CharacterView.StopState(IsShootingRifle);
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