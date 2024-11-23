using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private Unit _unit;
    private UnitAttack _unitAttack;
    private NavMeshAgent _navMesh;

    public NavMeshAgent NavMeshAgent => _navMesh;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _unit = GetComponent<Unit>();
        _unitAttack = GetComponent<UnitAttack>();
    }

    private void OnEnable()
    {
        StartCoroutine(SetNavMeshSpeed());
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

        if (distansToTarget > _unitAttack.AttackDistance)
        {
            _navMesh.speed = _unit.UnitConfig.Speed;
            _navMesh.SetDestination(_unit.Target.transform.position);
        }
        else
        {
            _navMesh.speed = 0;
        }
    }

    private void MoveForward()
    {
        float distansToCharacter = Vector3.Distance(transform.position, _unit.Character.transform.position);

        if (distansToCharacter < 5)
        {
            _navMesh.speed = _unit.UnitConfig.Speed;
            Vector3 forwardPosition = transform.position + Vector3.right * 5.0f;
            _navMesh.SetDestination(forwardPosition);
        }
    }

    private IEnumerator SetNavMeshSpeed()
    {
        yield return null; 
        _navMesh.speed = _unit.UnitConfig.Speed;
    }
}