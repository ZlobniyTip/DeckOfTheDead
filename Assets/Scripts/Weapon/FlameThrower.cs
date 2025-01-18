public class FlameThrower : RangeWeapon
{
    public override int Shoot()
    {
        if (_isShooting == false)
        {
            _audio.Play();
            _shotEffect.Play();
            _isShooting = true;
        }

        return _damage;
    }
}