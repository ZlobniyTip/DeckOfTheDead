public class IdlingState : MovementState
{
    const string IsIdlingMelle = "IsIdlingMelle";
    const string IsIdlingPistol = "IsIdlingPistol";
    const string IsIdlingRifle = "IsIdlingRifle";

    public IdlingState(IStateSwitcher stateSwitcher, Character character) : base(stateSwitcher, character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        switch (Character.CharacterShooting.CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                CharacterView.StartState(IsIdlingMelle);
                break;

            case WeaponType.Pistol:
                CharacterView.StartState(IsIdlingPistol);
                break;

            case WeaponType.Rifle:
                CharacterView.StartState(IsIdlingRifle);
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
                CharacterView.StopState(IsIdlingMelle);
                break;

            case WeaponType.Pistol:
                CharacterView.StopState(IsIdlingPistol);
                break;

            case WeaponType.Rifle:
                CharacterView.StopState(IsIdlingRifle);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (IsAttacking())
        {
            StateSwitcher.SwitchState<AttackState>();
        }

        if (IsMoving())
            return;

        StateSwitcher.SwitchState<RunningState>();
    }
}
