using System.Collections;
using Character;
using Enemy;
using Other;
using UnityEngine;
using UnityEngine.Events;

namespace Spawner
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private Health _target;
        [SerializeField] private Player _character;
        [SerializeField] private PlayerMovePoint[] _playerMovePoints;
        [SerializeField] private Zombie[] _prefabEnemies;
        [SerializeField] private int _delaySpawn = 2;

        private int _currentPoint = 0;
        private int _activeEnemies = 0;

        public event UnityAction<int, int> PointReached;
        public event UnityAction WaveCleared;
        public event UnityAction ZombieDied;

        private void OnEnable()
        {
            foreach (var playerMovePoint in _playerMovePoints)
            {
                playerMovePoint.PlayerCheckpointEntered += OnPlayerCheckpointEntered;
            }
        }

        private void OnDisable()
        {
            foreach (var playerMovePoint in _playerMovePoints)
            {
                playerMovePoint.PlayerCheckpointEntered -= OnPlayerCheckpointEntered;
            }
        }

        private void OnPlayerCheckpointEntered(Transform[] spawnPoints, int numberEnemiesInWave)
        {
            _currentPoint++;
            PointReached?.Invoke(_currentPoint, _playerMovePoints.Length);
            StartCoroutine(SpawnEnemyes(spawnPoints, numberEnemiesInWave));
        }

        private IEnumerator SpawnEnemyes(Transform[] spawnPoints, int numberEnemiesInWave)
        {
            var delay = new WaitForSeconds(_delaySpawn);
            _activeEnemies = numberEnemiesInWave;

            while (numberEnemiesInWave > 0)
            {
                Zombie enemy = Instantiate(
                    _prefabEnemies[Random.Range(0, _prefabEnemies.Length)],
                    spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position,
                    Quaternion.identity);

                enemy.ZombieSearch.InitializeStartTarget(_target);
                enemy.Died += OnHandleEnemyDeath;
                enemy.DieRewarded += OnGetLeaderboardScore;
                numberEnemiesInWave--;

                yield return delay;
            }
        }

        private void OnGetLeaderboardScore(int reward)
        {
            _character.GetLeaderboardScore(reward);
        }

        private void OnHandleEnemyDeath()
        {
            _activeEnemies--;
            ZombieDied?.Invoke();

            if (_activeEnemies <= 0)
                WaveCleared?.Invoke();
        }
    }
}