using System.Collections;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _prefabSpawnEffect;
    [SerializeField] private AudioSource _soundSpawn;

    private Deck _deck;

    private void Awake()
    {
        _deck = GetComponent<Deck>();
    }

    public void Spawn(Vector3 point, Unit prefabUnit)
    {
        _soundSpawn.Play();
        Instantiate(_prefabSpawnEffect, point + Vector3.up * 0.5f, Quaternion.identity);
        StartCoroutine(SetDelaySpawning(point, prefabUnit));
    }

    private IEnumerator SetDelaySpawning(Vector3 spawnPosition, Unit prefabUnit)
    {
        float amountDelayBeforeSpawning = 1f;

        yield return new WaitForSeconds(amountDelayBeforeSpawning);
        Unit unit = Instantiate(prefabUnit, spawnPosition, Quaternion.identity);
        unit.SetCharacter(_deck.Character);
    }
}
