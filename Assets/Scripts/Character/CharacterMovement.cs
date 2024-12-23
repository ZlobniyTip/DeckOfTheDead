using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Spawner _spawner;

    private NavMeshAgent _navMesh;
    private int _pointIndex = 0;
    private float _speed = 5;

    public NavMeshAgent NavMeshAgent => _navMesh;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {

        MoveToPoint();
    }

    private void OnEnable()
    {
        _spawner.WaveCleared += MoveToPoint; 
    }

    private void OnDisable()
    {
        _spawner.WaveCleared -= MoveToPoint; 
    }

    public void MoveToPoint()
    {
        if (_pointIndex < _points.Count)
        {
            _navMesh.speed = _speed;
            _navMesh.SetDestination(_points[_pointIndex].position);
            _pointIndex++;
        }
    }
}
