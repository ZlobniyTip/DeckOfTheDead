using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Enemy.Skills.Virus
{
    public class ZombieVirus : MonoBehaviour
    {
        private readonly HashSet<Unit> _subscribedObjects = new HashSet<Unit>();
        private readonly Collider[] _overlappedColliders = new Collider[10];

        [SerializeField] private ParticleSystem _virusEffect;
        [SerializeField] private Zombie _zombiePrefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _radius;
        [SerializeField] private float _duration;
        [SerializeField] private float _delayBeforeDestruction;

        private ParticleSystem _currentParticle;
        private float _timer = 0;

        private void Start()
        {
            _currentParticle = Instantiate(_virusEffect, transform.position, Quaternion.identity);
            StartCoroutine(Infection());
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private void OnDestroy()
        {
            UnsubscribeAll();
        }

        private IEnumerator Infection()
        {
            var delayBetweenDamage = new WaitForSeconds(_cooldown);
            var delayBeforeDestruction = new WaitForSeconds(_delayBeforeDestruction);

            while (_timer < _duration)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlappedColliders);
                Rigidbody rigidbody;

                for (int i = 0; i < count; i++)
                {
                    rigidbody = _overlappedColliders[i].attachedRigidbody;

                    if (rigidbody)
                    {
                        if (rigidbody.gameObject.TryGetComponent(out Unit enemy))
                        {
                            if (!_subscribedObjects.Contains(enemy))
                            {
                                _subscribedObjects.Add(enemy);
                                enemy.IntoZombieTurned += OnTurnedIntoZombie;
                            }

                            enemy.TakeDamage(_damage);
                        }
                    }
                }

                yield return delayBetweenDamage;
            }

            yield return delayBeforeDestruction;

            Destroy(_currentParticle);
            Destroy(gameObject);
        }

        private void OnTurnedIntoZombie(Unit unit)
        {
            if (_subscribedObjects.Contains(unit))
            {
                unit.IntoZombieTurned -= OnTurnedIntoZombie;
                _subscribedObjects.Remove(unit);
            }

            if (!unit.IsZombie)
            {
                Zombie zombie = Instantiate(_zombiePrefab, unit.transform.position, Quaternion.identity);
                zombie.ZombieSearch.InitializeStartTarget(unit);

                unit.IsZombie = true;
            }
        }

        private void UnsubscribeAll()
        {
            foreach (Unit obj in _subscribedObjects)
            {
                if (obj != null && obj.TryGetComponent(out Unit enemy))
                {
                    enemy.IntoZombieTurned -= OnTurnedIntoZombie;
                }
            }

            _subscribedObjects.Clear();
        }
    }
}