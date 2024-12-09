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

    private const string IsDiying = "IsDiying";

    private string _currentAnimationKey = string.Empty;

    [SerializeField] Animator _animator;

    public void PlauAttackAnimation(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Melle:
                SetAnimation(IsAttackingMelle);
                break;

            case WeaponType.Pistol:
                SetAnimation(IsShootingPistol);
                break;

            case WeaponType.Rifle:
                SetAnimation(IsShootingRifle);
                break;

            case WeaponType.Shotgun:
                SetAnimation(IsShootinShotgun);
                break;

            case WeaponType.HunterRifle:
                SetAnimation(IsShootingHunterRifle);
                break;

            case WeaponType.FlameThrower:
                SetAnimation(IsShootingHunterRifle);
                break;
        }
    }

    public void PlauRunningAnimation(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Melle:
                SetAnimation(IsRunningMelle);
                break;

            case WeaponType.Pistol:
                SetAnimation(IsRunningPistol);
                break;

            case WeaponType.Rifle:
                SetAnimation(IsRunningRifle);
                break;
        }
    }

    public void PlauIdlingAnimation(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Melle:
                SetAnimation(IsIdlingMelle);
                break;

            case WeaponType.Pistol:
                SetAnimation(IsIdlingPistol);
                break;

            case WeaponType.Rifle:
                SetAnimation(IsIdlingRifle);
                break;
        }
    }

    public void PlauDiyingAnimation()
    {
        SetAnimation(IsDiying);
    }

    private void SetAnimation( string animationName)
    {
        if (_currentAnimationKey == animationName)
            return;

        _currentAnimationKey = animationName;
        _animator.SetTrigger(_currentAnimationKey);
    }
}
