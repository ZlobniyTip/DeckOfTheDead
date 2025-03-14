using Weapons;

namespace Character.StateMachine.States
{
    public class IdlingState : MovementState
    {
        private const string IdlingMelle = "IsIdlingMelle";
        private const string IdlingPistol = "IsIdlingPistol";
        private const string IdlingRifle = "IsIdlingRifle";

        public IdlingState(IStateSwitcher stateSwitcher, Player character) 
            : base(stateSwitcher, character) { }

        public override void Enter()
        {
            base.Enter();

            switch (Character.CharacterShooting.CurrentWeapon.WeaponKind)
            {
                case WeaponType.Melle:
                    CharacterView.StartState(IdlingMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StartState(IdlingPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StartState(IdlingRifle);
                    break;

                default:
                    CharacterView.StartState(IdlingRifle);
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
                    CharacterView.StopState(IdlingMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StopState(IdlingPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StopState(IdlingRifle);
                    break;

                default:
                    CharacterView.StopState(IdlingRifle);
                    break;
            }
        }

        public override void Update()
        {
            base.Update();

            if (IsAttacking)
            {
                StateSwitcher.SwitchState<AttackState>();
            }

            if (MoveSpeed <= 0)
                return;

            StateSwitcher.SwitchState<RunningState>();
        }
    }
}