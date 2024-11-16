using System;
using UnityEngine;

public class PlayerMovePoint : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _numberEnemiesInWave = 8;

    public event Action<Transform[], int> PlayerOnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            PlayerOnPoint?.Invoke(_spawnPoints, _numberEnemiesInWave);
        }
    }
}
