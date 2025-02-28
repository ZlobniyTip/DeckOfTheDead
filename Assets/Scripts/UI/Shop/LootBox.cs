using Card;
using Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace UI.Shop
{
    public class LootBox : MonoBehaviour
    {
        private readonly List<Weapon> ChoosedWeapons = new();
        private readonly List<CardData> ChoosedCards = new();
        private readonly int CountWeapons = 5;
        private readonly int CountCards = 10;

        [SerializeField] private GameObject _prizesPanel;
        [SerializeField] private List<Weapon> _weapons;
        [SerializeField] private List<CardData> _cards;

        public List<Weapon> Weapons => ChoosedWeapons;
        public List<CardData> Cards => ChoosedCards;

        public void OpenLootBox()
        {
            for (int i = 0; i < CountWeapons; i++)
            {
                StartCoroutine(ChooseRandomWeapon(_weapons));
            }

            for (int i = 0; i < CountCards; i++)
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
            ChoosedWeapons.Add(weapons[weaponIndex]);
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
            ChoosedCards.Add(cards[cardIndex]);

            if (ChoosedCards.Count == 10)
            {
                _prizesPanel.SetActive(true);
            }
        }
    }
}