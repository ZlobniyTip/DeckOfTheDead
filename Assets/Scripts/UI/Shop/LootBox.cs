using System.Collections;
using System.Collections.Generic;
using Card;
using Save;
using UnityEngine;
using Weapons;

namespace UI.Shop
{
    public class LootBox : MonoBehaviour
    {
        private readonly List<Weapon> _choosedWeapons = new();
        private readonly List<CardData> _choosedCards = new();
        private readonly int _countWeapons = 5;
        private readonly int _countCards = 10;

        [SerializeField] private GameObject _prizesPanel;
        [SerializeField] private List<Weapon> _weapons;
        [SerializeField] private List<CardData> _cards;

        public List<Weapon> Weapons => _choosedWeapons;
        public List<CardData> Cards => _choosedCards;

        public void OpenLootBox()
        {
            for (int i = 0; i < _countWeapons; i++)
            {
                StartCoroutine(ChooseRandomWeapon(_weapons));
            }

            for (int i = 0; i < _countCards; i++)
            {
                StartCoroutine(ChooseRandomCard(_cards));
            }
        }

        private IEnumerator ChooseRandomWeapon(List<Weapon> weapons)
        {
            bool isFound = false;
            int weaponIndex = 0;

            while (isFound == false)
            {
                weaponIndex = Random.Range(0, weapons.Count);

                if (weapons[weaponIndex].State.Status == ItemStatus.NotPurchased)
                    isFound = true;

                yield return null;
            }

            weapons[weaponIndex].State.SetStatus(ItemStatus.Purchased);
            _choosedWeapons.Add(weapons[weaponIndex]);
        }

        private IEnumerator ChooseRandomCard(List<CardData> cards)
        {
            bool isFound = false;
            int cardIndex = 0;

            while (isFound == false)
            {
                cardIndex = Random.Range(0, cards.Count);

                if (cards[cardIndex].State.Status == ItemStatus.NotPurchased)
                    isFound = true;

                yield return null;
            }

            cards[cardIndex].State.SetStatus(ItemStatus.Purchased);
            _choosedCards.Add(cards[cardIndex]);

            if (_choosedCards.Count == 10)
            {
                _prizesPanel.SetActive(true);
            }
        }
    }
}