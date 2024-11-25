using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMesh;
    private Enemy _enemy;
    private float _speed = 2;
    private ZombieAttack _zombieAttack;

    public NavMeshAgent NavMeshAgent => _navMesh;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<Enemy>();
        _zombieAttack = GetComponent<ZombieAttack>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    public void StopMovement()
    {
        _navMesh.speed = 0;
    }

    private void MoveToTarget()
    {
        if(_enemy.Target == null)
        {
            _enemy.SetStartTarget();
        }

        float distansToTarget = Vector3.Distance(transform.position, _enemy.Target.transform.position);

        if (distansToTarget > _zombieAttack.AttackDistance)
        {
            _navMesh.speed = _speed;
            _navMesh.SetDestination(_enemy.Target.transform.position);
        }
        else
        {
            StopMovement();

        }
    }
}
