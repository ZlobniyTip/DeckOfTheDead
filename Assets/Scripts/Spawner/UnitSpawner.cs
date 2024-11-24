using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    private Deck _deck;

    private void Awake()
    {
        _deck = GetComponent<Deck>();
    }

    public void Spawn(Vector3 point, Unit prefabUnit)
    {
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
