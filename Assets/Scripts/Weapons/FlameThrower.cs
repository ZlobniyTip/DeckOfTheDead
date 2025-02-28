namespace Weapons
{
    public class FlameThrower : RangeWeapon
    {
        public override int Shoot()
        {
            if (IsShooting == false)
            {
                Audio.Play();
                ShotEffect.Play();
                IsShooting = true;
            }

            return Damage;
        }

        public void StopEffect()
        {
            ShotEffect.Stop();
            Audio.Stop();
        }
    }
}