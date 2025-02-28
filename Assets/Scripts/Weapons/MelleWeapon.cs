using System;

namespace Weapons
{
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

            if (Audio != null)
                Audio.Play();

            return Damage;
        }
    }
}