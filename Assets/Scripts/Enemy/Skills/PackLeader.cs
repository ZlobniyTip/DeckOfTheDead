using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Skills
{
    public class PackLeader : MonoBehaviour
    {
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

            while (true)
            {
                Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
                Rigidbody rigidbody;

                for (int i = 0; i < overlappedColliders.Length; i++)
                {
                    rigidbody = overlappedColliders[i].attachedRigidbody;
                    if (rigidbody)
                    {
                        if (rigidbody.gameObject.TryGetComponent(out ZombieAttack zombie))
                        {
                            if (!SubscribedObjects.Contains(zombie))
                            {
                                SubscribedObjects.Add(zombie);
                                zombie.BuffAttack(MultiplyAttackSpeed, MultiplyDamage);
                            }
                        }
                    }
                }

                yield return delay;
            }
        }
    }
}