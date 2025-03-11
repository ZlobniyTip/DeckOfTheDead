using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Enemy.Skills.Virus
{
    public class ZombieVirus : MonoBehaviour
    {
        private readonly HashSet<Unit> SubscribedObjects = new HashSet<Unit>();
        private readonly Collider[] OverlappedColliders = new Collider[10];

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
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, OverlappedColliders);
                Rigidbody rigidbody;

                for (int i = 0; i < count; i++)
                {
                    rigidbody = OverlappedColliders[i].attachedRigidbody;

                    if (rigidbody)
                    {
                        if (rigidbody.gameObject.TryGetComponent(out Unit enemy))
                        {
                            if (!SubscribedObjects.Contains(enemy))
                            {
                                SubscribedObjects.Add(enemy);
                                enemy.TurnIntoZombie += RiseZombie;
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

        private void RiseZombie(Unit unit)
        {
            if (SubscribedObjects.Contains(unit))
            {
                unit.TurnIntoZombie -= RiseZombie;
                SubscribedObjects.Remove(unit);
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
            foreach (Unit obj in SubscribedObjects)
            {
                if (obj != null && obj.TryGetComponent(out Unit enemy))
                {
                    enemy.TurnIntoZombie -= RiseZombie;
                }
            }

            SubscribedObjects.Clear();
        }
    }
}