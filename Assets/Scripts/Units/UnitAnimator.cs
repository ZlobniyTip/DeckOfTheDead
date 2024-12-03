using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    const string IsAttackingMelle = "IsMelleAttack";
    const string IsShootingPistol = "IsShootingPistol";
    const string IsShootingRifle = "IsShootingRifle";
    const string IsShootinShotgun = "IsShootinShotgun";
    const string IsShootingHunterRifle = "IsShootingHunterRifle";

    const string IsRunningMelle = "IsRunningMelle";
    const string IsRunningPistol = "IsRunningPistol";
    const string IsRunningRifle = "IsRunningRifle";

    const string IsIdlingMelle = "IsIdlingMelle";
    const string IsIdlingPistol = "IsIdlingPistol";
    const string IsIdlingRifle = "IsIdlingRifle";

    [SerializeField] Animator _animator;

    public void PlauAttackAnimation(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Melle:
                _animator.SetTrigger(IsAttackingMelle);
                break;

            case WeaponType.Pistol:
                _animator.SetTrigger(IsShootingPistol);
                break;

            case WeaponType.Rifle:
                _animator.SetTrigger(IsShootingRifle);
                break;

            case WeaponType.Shotgun:
                _animator.SetTrigger(IsShootinShotgun);
                break;

            case WeaponType.HunterRifle:
                _animator.SetTrigger(IsShootingHunterRifle);
                break;

            case WeaponType.FlameThrower:
                _animator.SetTrigger(IsShootingHunterRifle);
                break;
        }
    }

    public void PlauRunningAnimation(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Melle:
                _animator.SetTrigger(IsRunningMelle);
                break;

            case WeaponType.Pistol:
                _animator.SetTrigger(IsRunningPistol);
                break;

            case WeaponType.Rifle:
                _animator.SetTrigger(IsRunningRifle);
                break;
        }
    }

    public void PlauIdlingAnimation(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Melle:
                _animator.SetTrigger(IsIdlingMelle);
                break;

            case WeaponType.Pistol:
                _animator.SetTrigger(IsIdlingPistol);
                break;

            case WeaponType.Rifle:
                _animator.SetTrigger(IsIdlingRifle);
                break;
        }
    }
}
