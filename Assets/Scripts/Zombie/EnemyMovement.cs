using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ZombieSearchTarget))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private ZombieSearchTarget _zombieSearch;
    private NavMeshAgent _navMesh;
    private float _speed = 2;
    private ZombieAttack _zombieAttack;
    private Enemy _enemy;

    public NavMeshAgent NavMeshAgent => _navMesh;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _zombieSearch = GetComponent<ZombieSearchTarget>();
        _navMesh = GetComponent<NavMeshAgent>();
        _zombieAttack = GetComponent<ZombieAttack>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    public void StopMovement()
    {
        _navMesh.isStopped = true;
        _navMesh.speed = 0;
    }

    private void MoveToTarget()
    {
        if (_enemy.IsDiying)
        {
            StopMovement();
            return;
        }

        if (_zombieSearch.Target == null)
        {
            _zombieSearch.SetStartTarget();
        }

        float distansToTarget = Vector3.Distance(transform.position, _zombieSearch.Target.transform.position);

        if (distansToTarget > _zombieAttack.AttackDistance)
        {
            _navMesh.isStopped = false;
            _navMesh.speed = _speed;
            _navMesh.SetDestination(_zombieSearch.Target.transform.position);
        }
        else
        {
            StopMovement();
        }
    }
}
