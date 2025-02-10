using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVirus : MonoBehaviour
{
    [SerializeField] private ParticleSystem _virusEffect;
    [SerializeField] private Zombie _zombiePrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _radius;
    [SerializeField] private float _duration;
    [SerializeField] private float _delayBeforeDestruction;

    private HashSet<Unit> _subscribedObjects = new HashSet<Unit>();
    private ParticleSystem _currentParticle;
    private float _timer = 0;
    private Zombie _zombie;

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
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;
                if (rigidbody)
                {
                    if (rigidbody.gameObject.TryGetComponent(out Unit enemy))
                    {
                        if (!_subscribedObjects.Contains(enemy))
                        {
                            _subscribedObjects.Add(enemy);
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
        if (_subscribedObjects.Contains(unit))
        {
            _subscribedObjects.Remove(unit);
        }

        Zombie zombie = Instantiate(_zombiePrefab, unit.transform.position, Quaternion.identity);
        zombie.ZombieSearch.InitializeStartTarget(unit);
    }

    private void UnsubscribeAll()
    {
        foreach (Unit obj in _subscribedObjects)
        {
            if (obj != null && obj.TryGetComponent(out Unit enemy))
            {
                enemy.TurnIntoZombie -= RiseZombie;
            }
        }

        _subscribedObjects.Clear();
    }
}