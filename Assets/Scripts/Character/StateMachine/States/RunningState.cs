public class RunningState : MovementState
{
    const string IsRunningMelle = "IsRunningMelle";
    const string IsRunningPistol = "IsRunningPistol";
    const string IsRunningRifle = "IsRunningRifle";

    public RunningState(IStateSwitcher stateSwitcher, Character character) : base(stateSwitcher, character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        switch (Character.CharacterShooting.CurrentWeapon.WeaponType)
        {
            case WeaponType.Melle:
                CharacterView.StartState(IsRunningMelle);
                break;

            case WeaponType.Pistol:
                CharacterView.StartState(IsRunningPistol);
                break;

            case WeaponType.Rifle:
                CharacterView.StartState(IsRunningRifle);
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
                CharacterView.StopState(IsRunningMelle);
                break;

            case WeaponType.Pistol:
                CharacterView.StopState(IsRunningPistol);
                break;

            case WeaponType.Rifle:
                CharacterView.StopState(IsRunningRifle);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (IsMoving())
            StateSwitcher.SwitchState<IdlingState>();
    }
}
