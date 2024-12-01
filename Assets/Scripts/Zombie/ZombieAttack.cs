using System.Collections;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private ZombieSearchTarget _zombieSearchTarget;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBetweenAttack;
    [SerializeField] private float _attackDistance;

    private float _distance;
    private Enemy _enemy;

    public bool IsAttacking { get; private set; } = false;
    public float AttackDistance => _attackDistance;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void ActivateAttack(Health enemy)
    {
        StopCoroutine(_zombieSearchTarget.SearchTarget());
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_delayBetweenAttack);

        while (_zombieSearchTarget.Target != null)
        {
            transform.LookAt(_zombieSearchTarget.Target.transform);
            _distance = Vector3.Distance(transform.position, _zombieSearchTarget.Target.transform.position);

            if (_distance <= _attackDistance)
            {
                IsAttacking = true;

                _zombieSearchTarget.Target.TakeDamage(_damage);

                yield return delay;
            }
            else
            {
                IsAttacking = false;
                yield return null;
            }
        }

        StartCoroutine(_zombieSearchTarget.SearchTarget());
    }
}