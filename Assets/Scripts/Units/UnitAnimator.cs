using UnityEngine;
using Weapons;

namespace Units
{
    public class UnitAnimator : MonoBehaviour
    {
        private const string AttackingMelle = "IsMelleAttack";
        private const string ShootingPistol = "IsShootingPistol";
        private const string ShootingRifle = "IsShootingRifle";
        private const string ShootingShotgun = "IsShootinShotgun";
        private const string ShootingHunterRifle = "IsShootingHunterRifle";

        private const string RunningMelle = "IsRunningMelle";
        private const string RunningPistol = "IsRunningPistol";
        private const string RunningRifle = "IsRunningRifle";

        private const string IdlingMelle = "IsIdlingMelle";
        private const string IdlingPistol = "IsIdlingPistol";
        private const string IdlingRifle = "IsIdlingRifle";

        private const string Diying = "IsDiying";
        private const string Throws = "Throws";

        private string _currentAnimationKey = string.Empty;

        [SerializeField] Animator _animator;

        public void PlauAttackAnimation(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Melle:
                    SetAnimation(AttackingMelle);
                    break;

                case WeaponType.Pistol:
                    SetAnimation(ShootingPistol);
                    break;

                case WeaponType.Rifle:
                    SetAnimation(ShootingRifle);
                    break;

                case WeaponType.Shotgun:
                    SetAnimation(ShootingShotgun);
                    break;

                case WeaponType.HunterRifle:
                    SetAnimation(ShootingHunterRifle);
                    break;

                case WeaponType.FlameThrower:
                    SetAnimation(ShootingRifle);
                    break;
            }
        }

        public void PlauRunningAnimation(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Melle:
                    SetAnimation(RunningMelle);
                    break;

                case WeaponType.Pistol:
                    SetAnimation(RunningPistol);
                    break;

                case WeaponType.Rifle:
                    SetAnimation(RunningRifle);
                    break;

                case WeaponType.FlameThrower:
                    SetAnimation(RunningRifle);
                    break;

                case WeaponType.Shotgun:
                    SetAnimation(ShootingShotgun);
                    break;

                case WeaponType.HunterRifle:
                    SetAnimation(RunningRifle);
                    break;
            }
        }

        public void PlauIdlingAnimation(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Melle:
                    SetAnimation(IdlingMelle);
                    break;

                case WeaponType.Pistol:
                    SetAnimation(IdlingPistol);
                    break;

                case WeaponType.Rifle:
                    SetAnimation(IdlingRifle);
                    break;

                case WeaponType.FlameThrower:
                    SetAnimation(IdlingRifle);
                    break;

                case WeaponType.Shotgun:
                    SetAnimation(IdlingRifle);
                    break;

                case WeaponType.HunterRifle:
                    SetAnimation(IdlingRifle);

                    break;
            }
        }

        public void PlauDiyingAnimation()
        {
            SetAnimation(Diying);
        }

        public void PlayThrows()
        {
            SetAnimation(Throws);
        }

        private void SetAnimation(string animationName)
        {
            if (_currentAnimationKey == animationName)
                return;

            _currentAnimationKey = animationName;
            _animator.SetTrigger(_currentAnimationKey);
        }
    }
}