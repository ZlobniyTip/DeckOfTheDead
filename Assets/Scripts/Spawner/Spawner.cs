using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Health _target;
    [SerializeField] private PlayerMovePoint[] _playerMovePoints;
    [SerializeField] private Zombie[] _prefabEnemies;

    [SerializeField] private int _delaySpawn = 2;

    private int _currentPoint = 0;
    private int _activeEnemies = 0;

    public event UnityAction<int, int> ReachedPoint;
    public event UnityAction WaveCleared;
    public event UnityAction ZombieDie;

    private void OnEnable()
    {
        foreach (var playerMovePoint in _playerMovePoints)
        {
            playerMovePoint.PlayerOnPoint += StartSpawnEnemyes;
        }
    }

    private void OnDisable()
    {
        foreach (var playerMovePoint in _playerMovePoints)
        {
            playerMovePoint.PlayerOnPoint -= StartSpawnEnemyes;
        }
    }

    private void StartSpawnEnemyes(Transform[] spawnPoints, int numberEnemiesInWave)
    {
        _currentPoint++;
        ReachedPoint?.Invoke(_currentPoint, _playerMovePoints.Length);
        StartCoroutine(SpawnEnemyes(spawnPoints, numberEnemiesInWave));
    }

    private IEnumerator SpawnEnemyes(Transform[] spawnPoints, int numberEnemiesInWave)
    {
        var delay = new WaitForSeconds(_delaySpawn);
        _activeEnemies = numberEnemiesInWave;

        while (numberEnemiesInWave > 0)
        {
            Zombie enemy = Instantiate(_prefabEnemies[Random.Range(0, _prefabEnemies.Length)],
                spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position,
                Quaternion.identity);

            enemy.ZombieSearch.InitializeStartTarget(_target);
            enemy.Died += HandleEnemyDeath;
            numberEnemiesInWave--;

            yield return delay;
        }
    }

    private void HandleEnemyDeath()
    {
        _activeEnemies--;
        ZombieDie?.Invoke();

        if (_activeEnemies <= 0)
            WaveCleared?.Invoke(); 
    }
}