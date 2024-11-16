using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private List<Transform> _bulletPoints;

    public override int Shooting()
    {
        _shotEffect.Play();

        if (_bulletPoints.Count > 1)
        {
            for (int i = 0; i < _bulletPoints.Count; i++)
            {
                Instantiate(_bullet, _bulletPoints[i].position, _bulletPoints[i].rotation);
            }
        }

        Instantiate(_bullet, _bulletPoints[0].position, _bulletPoints[0].rotation);

        return _damage;
    }
}