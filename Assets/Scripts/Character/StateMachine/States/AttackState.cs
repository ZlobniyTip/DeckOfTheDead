using Weapons;

namespace Character.StateMachine.States
{
    public class AttackState : MovementState
    {
        private const string AttackingMelle = "IsMelleAttack";
        private const string ShootingPistol = "IsShootingPistol";
        private const string ShootingRifle = "IsShootingRifle";
        private const string ShootingFlameThrower = "IsShootingFlameThrower";
        private const string ShootinShotgun = "IsShootinShotgun";
        private const string ShootingHunterRifle = "IsShootingHunterRifle";

        public AttackState(IStateSwitcher stateSwitcher, Player character)
            : base(stateSwitcher, character) { }

        public override void Enter()
        {
            base.Enter();

            switch (Character.CharacterShooting.CurrentWeapon.WeaponKind)
            {
                case WeaponType.Melle:
                    CharacterView.StartState(AttackingMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StartState(ShootingPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StartState(ShootingRifle);
                    break;

                case WeaponType.Shotgun:
                    CharacterView.StartState(ShootinShotgun);
                    break;

                case WeaponType.FlameThrower:
                    CharacterView.StartState(ShootingFlameThrower);
                    break;

                default:
                    CharacterView.StartState(ShootingHunterRifle);
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
                    CharacterView.StopState(AttackingMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StopState(ShootingPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StopState(ShootingRifle);
                    break;

                case WeaponType.Shotgun:
                    CharacterView.StopState(ShootinShotgun);
                    break;

                default:
                    CharacterView.StopState(ShootingHunterRifle);
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
}