using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Skills
{
    public class PackLeader : MonoBehaviour
    {
        private readonly Collider[] _overlappedColliders = new Collider[10];
        private readonly HashSet<ZombieAttack> _subscribedObjects = new HashSet<ZombieAttack>();
        private readonly int _cooldown = 1;
        private readonly int _multiplyAttackSpeed = 2;
        private readonly int _multiplyDamage = 2;

        [SerializeField] private float _radius;

        private void Start()
        {
            StartCoroutine(ApplyReinforcement());
        }

        private IEnumerator ApplyReinforcement()
        {
            var delay = new WaitForSeconds(_cooldown);

            while (enabled)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (!_overlappedColliders[i].
                        TryGetComponent(out Rigidbody rigidbody) || rigidbody == null)
                        continue;

                    if (!rigidbody.gameObject.
                        TryGetComponent(out ZombieAttack zombie) || _subscribedObjects.Contains(zombie))
                        continue;

                    _subscribedObjects.Add(zombie);
                    zombie.BuffAttack(_multiplyAttackSpeed, _multiplyDamage);
                }

                yield return delay;
            }
        }
    }
}