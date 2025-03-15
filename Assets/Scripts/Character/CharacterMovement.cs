using Spawner;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterMovement : MonoBehaviour
    {
        private readonly float Speed = 4.5f;

        [SerializeField] private List<Transform> _points;
        [SerializeField] private ZombieSpawner _spawner;

        private NavMeshAgent _navMesh;
        private int _pointIndex = 0;

        public NavMeshAgent NavMeshAgent => _navMesh;

        private void Awake()
        {
            _navMesh = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            OnMoveToPoint();
            _spawner.WaveCleared += OnMoveToPoint;
        }

        private void OnDisable()
        {
            _spawner.WaveCleared -= OnMoveToPoint;
        }

        private void OnMoveToPoint()
        {
            if (_pointIndex < _points.Count)
            {
                _navMesh.speed = Speed;
                _navMesh.SetDestination(_points[_pointIndex].position);
                _pointIndex++;
            }
        }

        public void StopMove()
        {
            _navMesh.speed = 0;
        }
    }
}