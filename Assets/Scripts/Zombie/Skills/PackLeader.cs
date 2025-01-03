using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackLeader : MonoBehaviour
{
    [SerializeField] private float _radius;

    private HashSet<ZombieAttack> _subscribedObjects = new HashSet<ZombieAttack>();
    private int _cooldown = 1;
    private int _multiplyAttackSpeed = 2;
    private int _multiplyDamage = 2;

    private void Start()
    {
        StartCoroutine(ApplyReinforcement());
    }

    private IEnumerator ApplyReinforcement()
    {
        var delay = new WaitForSeconds(_cooldown);

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
                        if (!_subscribedObjects.Contains(zombie))
                        {
                            _subscribedObjects.Add(zombie);
                            zombie.BuffAttack(_multiplyAttackSpeed, _multiplyDamage);
                        }
                    }
                }
            }

            yield return delay;
        }
    }
}