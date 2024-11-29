using System.Collections;
using UnityEngine;

public class ZombieSearchTarget : MonoBehaviour
{
    [SerializeField] private ZombieAttack _zombieAttack;
    [SerializeField] private float _radius;

    private Health _target;
    private Health _defaultTarget;

    public Health Target => _target;

    private void Start()
    {
        StartCoroutine(SearchTarget());
    }

    public IEnumerator SearchTarget()
    {
        while (_target as Character)
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
                            InitializeTarget(enemy);
                            _zombieAttack.ActivateAttack(enemy);
                        }
                    }
                }
            }

            yield return null;
        }
    }

    public void InitializeStartTarget(Health target)
    {
        _target = target;
        _defaultTarget = target;
    }

    public void InitializeTarget(Health target)
    {
        _target = target;
    }

    public void SetStartTarget()
    {
        _target = _defaultTarget;
    }
}