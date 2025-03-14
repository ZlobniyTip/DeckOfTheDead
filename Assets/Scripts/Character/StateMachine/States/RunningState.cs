using Weapons;

namespace Character.StateMachine.States
{
    public class RunningState : MovementState
    {
        private const string RunningMelle = "IsRunningMelle";
        private const string RunningPistol = "IsRunningPistol";
        private const string RunningRifle = "IsRunningRifle";

        public RunningState(IStateSwitcher stateSwitcher, Player character) 
            : base(stateSwitcher, character) { }

        public override void Enter()
        {
            base.Enter();

            switch (Character.CharacterShooting.CurrentWeapon.WeaponKind)
            {
                case WeaponType.Melle:
                    CharacterView.StartState(RunningMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StartState(RunningPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StartState(RunningRifle);
                    break;

                default:
                    CharacterView.StartState(RunningRifle);
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
                    CharacterView.StopState(RunningMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StopState(RunningPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StopState(RunningRifle);
                    break;

                default:
                    CharacterView.StopState(RunningRifle);
                    break;
            }
        }

        public override void Update()
        {
            base.Update();

            if (MoveSpeed <= 0)
                StateSwitcher.SwitchState<IdlingState>();
        }
    }
}