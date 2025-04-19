using System.Collections;
using Enemy;
using UnityEngine;

namespace Weapons
{
    public class Molotov : MonoBehaviour
    {
        private readonly Collider[] _overlappedColliders = new Collider[10];

        [SerializeField] private ParticleSystem _burningEffect;
        [SerializeField] private ParticleSystem _radiusEffect;
        [SerializeField] private float _delayBetweenDamage;
        [SerializeField] private float _radius;
        [SerializeField] private int _damage;
        [SerializeField] private float _durationBurning;

        private float _timer = 0;

        private void OnDestroy()
        {
            StopCoroutine(Flame());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.contacts.Length > 0)
            {
                StartCoroutine(Flame());
                Instantiate(_burningEffect, transform);
                Instantiate(_radiusEffect, transform.localPosition, Quaternion.identity);

                Destroy(gameObject);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private IEnumerator Flame()
        {
            while (_timer < _durationBurning)
            {
                var delay = new WaitForSeconds(_delayBetweenDamage);

                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlappedColliders);
                Rigidbody rigidbody;

                for (int i = 0; i < count; i++)
                {
                    rigidbody = _overlappedColliders[i].attachedRigidbody;

                    if (rigidbody)
                    {
                        if (rigidbody.gameObject.TryGetComponent(out Zombie enemy))
                        {
                            enemy.TakeDamage(_damage);

                            yield return delay;
                        }
                    }
                }

                yield return null;
            }
        }
    }
}