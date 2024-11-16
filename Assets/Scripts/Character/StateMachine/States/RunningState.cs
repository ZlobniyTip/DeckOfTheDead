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

        switch (Character.CharacterShooting.CurrentWeapon.WeaponStatus)
        {
            case WeaponStatus.Melle:
                CharacterView.StartState(IsRunningMelle);
                break;

            case WeaponStatus.Pistol:
                CharacterView.StartState(IsRunningPistol);
                break;

            case WeaponStatus.Rifle:
                CharacterView.StartState(IsRunningRifle);
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();

        switch (Character.CharacterShooting.CurrentWeapon.WeaponStatus)
        {
            case WeaponStatus.Melle:
                CharacterView.StopState(IsRunningMelle);
                break;

            case WeaponStatus.Pistol:
                CharacterView.StopState(IsRunningPistol);
                break;

            case WeaponStatus.Rifle:
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
