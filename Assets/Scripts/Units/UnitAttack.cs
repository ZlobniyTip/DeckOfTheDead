using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [SerializeField] private float _delayBetweenAttack;
    [SerializeField] private float _attackDistance;

    UnitSearchTarget _searchTarget;
    private UnitMovement _unitMovement;
    private Unit _unit;
    private float _distance;

    public float AttackDistance => _attackDistance;
    public bool IsAttacking { get; private set; } = false;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _unitMovement = GetComponent<UnitMovement>();
        _searchTarget = GetComponent<UnitSearchTarget>();
    }

    public void ActivateAttack(Enemy enemy)
    {
        StopCoroutine(_searchTarget.SearchTarget());
        StartCoroutine(Attacking());
    }

    private void Start()
    {
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_delayBetweenAttack);

        while (_unit.Target != null)
        {
            transform.LookAt(_unit.Target.transform);
            _distance = Vector3.Distance(transform.position, _unit.Target.transform.position);

            if (_distance <= _attackDistance)
            {
                IsAttacking = true;

                _unit.Target.TakeDamage(_unit.UnitConfig.Damage);

                yield return delay;
            }

            yield return null;
        }

        IsAttacking = false;
        StartCoroutine(_searchTarget.SearchTarget());
    }
}
