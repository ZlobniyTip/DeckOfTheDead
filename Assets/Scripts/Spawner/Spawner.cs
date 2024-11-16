using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Character _target;
    [SerializeField] private PlayerMovePoint[] _playerMovePoints;
    [SerializeField] private Enemy[] _prefabEnemies;

    [SerializeField] private int _delaySpawn = 2;

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
