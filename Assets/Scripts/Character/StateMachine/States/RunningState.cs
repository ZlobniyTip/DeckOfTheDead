using Weapons;

namespace Character.StateMachine.States
{
    public class RunningState : MovementState
    {
        private const string IsRunningMelle = "IsRunningMelle";
        private const string IsRunningPistol = "IsRunningPistol";
        private const string IsRunningRifle = "IsRunningRifle";

        public RunningState(IStateSwitcher stateSwitcher, Player character) : base(stateSwitcher, character)
        {
        }

        public override void Enter()
        {
            base.Enter();

            switch (Character.CharacterShooting.CurrentWeapon.WeaponKind)
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

                default:
                    CharacterView.StartState(IsRunningRifle);
                    break;
            }

            CurrentWeapon = Character.CharacterShooting.CurrentWeapon;
        }

        public override void Exit()
        {
            base.Exit();

            switch (CurrentWeapon.WeaponKind)
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

                default:
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
}