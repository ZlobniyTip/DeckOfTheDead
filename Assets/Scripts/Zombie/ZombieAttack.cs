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
        //_enemy.InitializeTarget(enemy);  
        StopCoroutine(_zombieSearchTarget.SearchTarget());
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_delayBetweenAttack);

        while (_enemy.Target != null)
        {
            transform.LookAt(_enemy.Target.transform);
            _distance = Vector3.Distance(transform.position, _enemy.Target.transform.position);

            if (_distance <= _attackDistance)
            {
                IsAttacking = true;

                _enemy.Target.TakeDamage(_damage);

                yield return delay;
            }
            else
            {
                yield return null;

            }
        }

        IsAttacking = false;
        StartCoroutine(_zombieSearchTarget.SearchTarget());
    }
}