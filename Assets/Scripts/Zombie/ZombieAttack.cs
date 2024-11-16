using System.Collections;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private ZombieSearchTarget _zombieSearchTarget;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBetweenAttack;
    [SerializeField] private float _attackDistance;

    private Character _currentTarget;
    private float _distance;

    public bool IsAttacking { get; private set; } = false;

    public void ActivateAttack(Character enemy)
    {
        _currentTarget = enemy;
        StopCoroutine(_zombieSearchTarget.SearchTarget());
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_delayBetweenAttack);

        while (_currentTarget != null)
        {
            transform.LookAt(_currentTarget.transform);
            _distance = Vector3.Distance(transform.position, _currentTarget.transform.position);

            if (_distance <= _attackDistance)
            {
                _enemyMovement.NavMeshAgent.speed = 0;
                IsAttacking = true;

                _currentTarget.TakeDamage(_damage);

                yield return delay;
            }

            yield return null;
        }

        IsAttacking = false;
        StartCoroutine(_zombieSearchTarget.SearchTarget());
    }
}