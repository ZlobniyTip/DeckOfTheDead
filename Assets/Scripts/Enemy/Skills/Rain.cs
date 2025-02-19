using System.Collections;
using UnityEngine;

namespace Enemy.Skills
{
    public class Rain : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _rainEffect;
        [SerializeField] private int _damage;
        [SerializeField] private float _radius;
        [SerializeField] private float _delayBetweenDamage;
        [SerializeField] private AudioSource _source;

        private ParticleSystem _currentRain;
        private float _lifeTime = 5;
        private float _timer = 0;

        private void Start()
        {
            _source.Play();
            _currentRain = Instantiate(_rainEffect, transform.position, Quaternion.identity);
            StartCoroutine(StartRain());
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private IEnumerator StartRain()
        {
            while (_lifeTime > _timer)
            {
                var delay = new WaitForSeconds(_delayBetweenDamage);

                Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
                Rigidbody rigidbody;

                for (int i = 0; i < overlappedColliders.Length; i++)
                {
                    rigidbody = overlappedColliders[i].attachedRigidbody;

                    if (rigidbody)
                    {
                        if (rigidbody.gameObject.TryGetComponent(out Zombie enemy))
                        {
                            enemy.TakeDamage(_damage);
                        }
                    }
                }

                yield return delay;
            }

            Destroy(_currentRain);
            Destroy(gameObject);
        }
    }
}