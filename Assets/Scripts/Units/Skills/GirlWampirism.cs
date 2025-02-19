using Enemy;
using System.Collections;
using UnityEngine;

namespace Units.Skills
{
    [RequireComponent(typeof(Unit))]
    public class GirlWampirism : Skill
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private int _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _radius;

        private Unit _unit;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        private void Start()
        {
            StartCoroutine(StealingLife());
            Instantiate(_particleSystem, transform);
        }

        private IEnumerator StealingLife()
        {
            while (true)
            {
                var delay = new WaitForSeconds(_cooldown);

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
                            _unit.TakeHeal(_damage);
                        }
                    }
                }

                yield return delay;
            }
        }
    }
}