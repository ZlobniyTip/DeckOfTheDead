using System;

public class MelleWeapon : Weapon
{
    public event Action MelleAttack;

    private void OnEnable()
    {
        MelleAttack += ReportImpact;
    }

    private void OnDisable()
    {
        MelleAttack += ReportImpact;
    }

    public override int Shoot()
    {
        MelleAttack?.Invoke();
        _audio.Play();

        return _damage;
    }
}