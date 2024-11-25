using System.Collections;
using UnityEngine;

public class ZombieSearchTarget : MonoBehaviour
{
    [SerializeField] private ZombieAttack _zombieAttack;
    [SerializeField] private float _radius;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        StartCoroutine(SearchTarget());
    }

    public IEnumerator SearchTarget()
    {
        while (_enemy.Target as Character)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;

                if (rigidbody)
                {
                    if (rigidbody.gameObject.TryGetComponent(out Health enemy))
                    {
                        if (enemy as Unit)
                        {
                            _enemy.InitializeTarget(enemy);
                            _zombieAttack.ActivateAttack(enemy);
                        }
                    }
                }
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}