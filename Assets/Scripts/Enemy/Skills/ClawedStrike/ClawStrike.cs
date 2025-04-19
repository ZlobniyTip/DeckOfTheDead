using System.Collections;
using Units;
using Units.Skills;
using UnityEngine;

namespace Enemy.Skills.ClawedStrike
{
    public class ClawStrike : Skill
    {
        private readonly Collider[] _overlappedColliders = new Collider[10];
        private readonly bool _isWorks = true;

        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private Bleeding _bleeding;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _radius;

        private Bleeding _currentBleeding;

        private void Start()
        {
            StartCoroutine(AttackWithClaws());
        }

        private IEnumerator AttackWithClaws()
        {
            var delay = new WaitForSeconds(_cooldown);

            while (_isWorks)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (!_overlappedColliders[i].TryGetComponent(out Rigidbody rigidbody) || rigidbody == null)
                        continue;

                    if (!rigidbody.gameObject.TryGetComponent(out Unit enemy))
                        continue;

                    Instantiate(_hitEffect, transform);
                    _currentBleeding = Instantiate(_bleeding, enemy.transform);
                    _currentBleeding.GetLinkUnit(enemy);
                }

                yield return delay;
            }
        }
    }
}