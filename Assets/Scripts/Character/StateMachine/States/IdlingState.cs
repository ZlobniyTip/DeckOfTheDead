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

        switch (Character.CharacterShooting.CurrentWeapon.WeaponStatus)
        {
            case WeaponStatus.Melle:
                CharacterView.StartState(IsIdlingMelle);
                break;

            case WeaponStatus.Pistol:
                CharacterView.StartState(IsIdlingPistol);
                break;

            case WeaponStatus.Rifle:
                CharacterView.StartState(IsIdlingRifle);
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();

        switch (Character.CharacterShooting.CurrentWeapon.WeaponStatus)
        {
            case WeaponStatus.Melle:
                CharacterView.StopState(IsIdlingMelle);
                break;

            case WeaponStatus.Pistol:
                CharacterView.StopState(IsIdlingPistol);
                break;

            case WeaponStatus.Rifle:
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
