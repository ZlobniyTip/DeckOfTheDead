using System.Collections;
using UnityEngine;

public class ZombieSearchTarget : MonoBehaviour
{
    [SerializeField] private ZombieAttack _zombieAttack;
    [SerializeField] private float _radius;

    private Health _target;
    private Health _startTarget;

    public Health Target => _target;
    public Health StartTarget => _startTarget;
    public bool SearchingTarget { get; private set; } = true;

    private void Start()
    {
        StartCoroutine(SearchTarget());
    }

    public IEnumerator SearchTarget()
    {
        SearchingTarget = true;

        while (SearchingTarget)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;
            Health unitTarget = null;

            foreach (var collider in overlappedColliders)
            {
                rigidbody = collider.attachedRigidbody;

                if (rigidbody && rigidbody.gameObject.TryGetComponent(out Health health))
                {
                    if (health is Unit unit || health is Character && health.IsDiying == false)
                    {
                        unitTarget = health;
                        break;
                    }
                }
            }

            if (unitTarget != null)
            {
                InitializeTarget(unitTarget);
                _zombieAttack.ActivateAttack();
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
        if (_target != null)
            _target.Died -= ClearTarget;

        if (target != null && !target.IsDiying)
        {
            _target = target;
            _target.Died += ClearTarget;
        }
    }

    public void SetStartTarget()
    {
        _target = _startTarget;
    }

    public void ClearTarget()
    {
        _target.Died -= ClearTarget;
        SetStartTarget();
    }

    private void OnDisable()
    {
        SearchingTarget = false;
    }
}