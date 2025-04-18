using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Skills
{
    public class PackLeader : MonoBehaviour
    {
        private readonly Collider[] OverlappedColliders = new Collider[10];
        private readonly HashSet<ZombieAttack> SubscribedObjects = new HashSet<ZombieAttack>();
        private readonly int Cooldown = 1;
        private readonly int MultiplyAttackSpeed = 2;
        private readonly int MultiplyDamage = 2;

        [SerializeField] private float _radius;

        private void Start()
        {
            StartCoroutine(ApplyReinforcement());
        }

        private IEnumerator ApplyReinforcement()
        {
            var delay = new WaitForSeconds(Cooldown);

            while (enabled)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, OverlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (!OverlappedColliders[i].
                        TryGetComponent(out Rigidbody rigidbody) || rigidbody == null)
                        continue;

                    if (!rigidbody.gameObject.
                        TryGetComponent(out ZombieAttack zombie) || SubscribedObjects.Contains(zombie))
                        continue;

                    SubscribedObjects.Add(zombie);
                    zombie.BuffAttack(MultiplyAttackSpeed, MultiplyDamage);
                }

                yield return delay;
            }
        }
    }
}