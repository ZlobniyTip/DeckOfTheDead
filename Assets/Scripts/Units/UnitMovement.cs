using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private Unit _unit;
    private UnitAttack _nitAttack;
    private NavMeshAgent _navMesh;
    private float _speed;

    public NavMeshAgent NavMeshAgent => _navMesh;
    public float Speed => _speed;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _unit = GetComponent<Unit>();
        _nitAttack = GetComponent<UnitAttack>();
    }

    private void Start()
    {
        _navMesh.speed = _unit.UnitConfig.Speed;
    }

    private void Update()
    {
        if (_unit.Target == null)
            MoveForward();
        else
            MoveToTarget();
    }

    private void MoveToTarget()
    {
        float distansToTarget = Vector3.Distance(transform.position, _unit.Target.transform.position);

        if ((distansToTarget > _nitAttack.AttackDistance))
        {
            _navMesh.SetDestination(_unit.Target.transform.position);
        }
    }

    private void MoveForward()
    {
        float distansToCharacter = Vector3.Distance(transform.position, _unit.Character.transform.position);

        if (distansToCharacter < 5)
        {
            Vector3 forwardPosition = transform.position + Vector3.right * 5.0f;
            _navMesh.SetDestination(forwardPosition);
        }
    }
}