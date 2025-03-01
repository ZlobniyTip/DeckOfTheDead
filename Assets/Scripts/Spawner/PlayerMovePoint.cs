using System;
using Character;
using UnityEngine;

namespace Spawner
{
    public class PlayerMovePoint : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private int _numberEnemiesInWave = 8;
        [SerializeField] private AudioSource _zombieSource;

        public event Action<Transform[], int> PlayerOnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player character))
            {
                PlayerOnPoint?.Invoke(_spawnPoints, _numberEnemiesInWave);
                character.Movement.StopMove();
                _zombieSource.Play();
            }
        }
    }
}