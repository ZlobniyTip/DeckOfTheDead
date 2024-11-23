using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Health _target;
    [SerializeField] private PlayerMovePoint[] _playerMovePoints;
    [SerializeField] private Enemy[] _prefabEnemies;

    [SerializeField] private int _delaySpawn = 2;

    private int _currentPoint = 0;

    public event UnityAction<int, int> ReachedPoint;

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

        while (numberEnemiesInWave > 0)
        {
            Enemy enemy = Instantiate(_prefabEnemies[Random.Range(0, _prefabEnemies.Length)],
                spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position,
                Quaternion.identity);

            enemy.InitializeTarget(_target);
            numberEnemiesInWave--;

            yield return delay;
        }
    }
}
