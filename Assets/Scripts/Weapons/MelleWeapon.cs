using System;

namespace Weapons
{
    public class MelleWeapon : Weapon
    {
        public event Action Attacked;

        private void OnEnable()
        {
            Attacked += OnReportImpact;
        }

        private void OnDisable()
        {
            Attacked += OnReportImpact;
        }

        public override int Shoot()
        {
            Attacked?.Invoke();

            if (Audio != null)
                Audio.Play();

            return Damage;
        }
    }
}