using Weapons;

namespace Character.StateMachine.States
{
    public class AttackState : MovementState
    {
        private const string IsAttackingMelle = "IsMelleAttack";
        private const string IsShootingPistol = "IsShootingPistol";
        private const string IsShootingRifle = "IsShootingRifle";
        private const string IsShootingFlameThrower = "IsShootingFlameThrower";
        private const string IsShootinShotgun = "IsShootinShotgun";
        private const string IsShootingHunterRifle = "IsShootingHunterRifle";

        public AttackState(IStateSwitcher stateSwitcher, Player character) : base(stateSwitcher, character)
        {
        }

        public override void Enter()
        {
            base.Enter();

            switch (Character.CharacterShooting.CurrentWeapon.WeaponKind)
            {
                case WeaponType.Melle:
                    CharacterView.StartState(IsAttackingMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StartState(IsShootingPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StartState(IsShootingRifle);
                    break;

                case WeaponType.Shotgun:
                    CharacterView.StartState(IsShootinShotgun);
                    break;

                case WeaponType.FlameThrower:
                    CharacterView.StartState(IsShootingFlameThrower);
                    break;

                default:
                    CharacterView.StartState(IsShootingHunterRifle);
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
                    CharacterView.StopState(IsAttackingMelle);
                    break;

                case WeaponType.Pistol:
                    CharacterView.StopState(IsShootingPistol);
                    break;

                case WeaponType.Rifle:
                    CharacterView.StopState(IsShootingRifle);
                    break;

                case WeaponType.Shotgun:
                    CharacterView.StopState(IsShootinShotgun);
                    break;

                default:
                    CharacterView.StopState(IsShootingHunterRifle);
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