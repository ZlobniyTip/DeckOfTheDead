using System.Collections;
using UnityEngine;

namespace Enemy.Skills
{
    public class Rain : MonoBehaviour
    {
        private readonly float _lifeTime = 5;
        private readonly Collider[] overlappedColliders = new Collider[10];

        [SerializeField] private ParticleSystem _rainEffect;
        [SerializeField] private int _damage;
        [SerializeField] private float _radius;
        [SerializeField] private float _delayBetweenDamage;
        [SerializeField] private AudioSource _source;

        private ParticleSystem _currentRain;
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
            var delay = new WaitForSeconds(_delayBetweenDamage);

            while (_lifeTime > _timer)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, overlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (!overlappedColliders[i].TryGetComponent(out Rigidbody rigidbody) || rigidbody == null)
                        continue;

                    if (rigidbody.gameObject.TryGetComponent(out Zombie enemy))
                    {
                        enemy.TakeDamage(_damage);
                    }
                }

                yield return delay;
            }

            Destroy(_currentRain);
            Destroy(gameObject);
        }
    }
}