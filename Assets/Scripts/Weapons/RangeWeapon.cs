using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private ParticleSystem _shotEffect;
        [SerializeField] private ParticleSystem _shotEffect2;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private List<Transform> _bulletPoints;

        protected ParticleSystem ShotEffect => _shotEffect;

        public override int Shoot()
        {
            if (Audio != null)
                Audio.Play();

            ShotEffect.Play();

            if (_shotEffect2 != null)
                _shotEffect2.Play();

            if (_bulletPoints.Count > 1)
            {
                for (int i = 0; i < _bulletPoints.Count; i++)
                {
                    Instantiate(_bullet, _bulletPoints[i].position, _bulletPoints[i].rotation);
                }

                return Damage;
            }

            Instantiate(_bullet, _bulletPoints[0].position, _bulletPoints[0].rotation);

            return Damage;
        }
    }
}