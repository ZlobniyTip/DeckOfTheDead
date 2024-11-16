using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMesh;
    private Enemy _enemy;
    private float _speed = 2;

    public NavMeshAgent NavMeshAgent => _navMesh;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        MoveToPoint();
    }

    public void MoveToPoint()
    {
        _navMesh.speed = _speed;
        _navMesh.SetDestination(_enemy.Target.transform.position);
    }
}
