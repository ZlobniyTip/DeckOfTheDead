using System;
using System.Collections;
using Card;
using Deck;
using Units;
using UnityEngine;

namespace Spawner
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _prefabSpawnEffect;
        [SerializeField] private AudioSource _soundSpawn;

        private PlayerDeck _deck;

        public event Action UsedCard;

        private void Awake()
        {
            _deck = GetComponent<PlayerDeck>();
        }

        public void Spawn(Vector3 point, Unit prefabUnit, CardView cardView)
        {
            UsedCard?.Invoke();
            _soundSpawn.Play();
            Instantiate(_prefabSpawnEffect, point + Vector3.up * 0.5f, Quaternion.identity);
            StartCoroutine(SetDelaySpawning(point, prefabUnit, cardView));
        }

        private IEnumerator SetDelaySpawning(Vector3 spawnPosition, Unit prefabUnit, CardView cardView)
        {
            float amountDelayBeforeSpawning = 1f;

            yield return new WaitForSeconds(amountDelayBeforeSpawning);
            Unit unit = Instantiate(prefabUnit, spawnPosition, Quaternion.identity);
            unit.GetCardView(cardView);
            unit.SetCharacter(_deck.Character);
        }
    }
}