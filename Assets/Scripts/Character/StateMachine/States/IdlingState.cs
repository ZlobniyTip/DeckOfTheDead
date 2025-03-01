using Weapons;

namespace Character.StateMachine.States
{
    public class IdlingState : MovementState
    {
        private const string IsIdlingMelle = "IsIdlingMelle";
        private const string IsIdlingPistol = "IsIdlingPistol";
        private const string IsIdlingRifle = "IsIdlingRifle";

        public IdlingState(IStateSwitcher stateSwitcher, Player character) : base(stateSwitcher, character)
        {
        }

        public override void Enter()
        {
            base.Enter();

            switch (Character.CharacterShooting.CurrentWeapon.WeaponKind)
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

                default:
                    CharacterView.StartState(IsIdlingRifle);
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
                    CharacterView.StopState(IsIdlingMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StopState(IsIdlingPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StopState(IsIdlingRifle);
                    break;

                default:
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
}