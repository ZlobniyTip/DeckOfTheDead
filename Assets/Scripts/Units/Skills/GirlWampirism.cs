using System.Collections;
using Enemy;
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
        private Collider[] _overlappedColliders = new Collider[10];

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
            while (enabled)
            {
                var delay = new WaitForSeconds(_cooldown);

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
                            _unit.TakeHeal(_damage);
                        }
                    }
                }

                yield return delay;
            }
        }
    }
}