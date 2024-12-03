using System.Collections;
using UnityEngine;

public class ZombieSearchTarget : MonoBehaviour
{
    [SerializeField] private ZombieAttack _zombieAttack;
    [SerializeField] private float _radius;

    private Enemy _enemy;
    private bool _searchingTarget = true;
    private Health _target;
    private Health _startTarget;

    public Health Target => _target;

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
        while (_searchingTarget)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;
            Health unitTarget = null;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;

                if (rigidbody && rigidbody.gameObject.TryGetComponent(out Health health))
                {
                    if (health is Unit)
                    {
                        unitTarget = health;
                        break;
                    }
                }
            }

            if (unitTarget != null)
            {
                InitializeTarget(unitTarget);
                _zombieAttack.ActivateAttack(unitTarget); 
            }
            else if (Target == null) 
            {
                SetStartTarget();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void InitializeStartTarget(Health target)
    {
        _target = target;
        _startTarget = target;
    }

    public void InitializeTarget(Health target)
    {
        _target = target;
    }

    public void SetStartTarget()
    {
        _target = _startTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}