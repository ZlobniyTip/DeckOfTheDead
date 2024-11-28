using UnityEngine;

public class FlameThrower : Weapon
{
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private Transform _bulletPoint;

    public override int Shooting()
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