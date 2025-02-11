using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private GameObject _prizesPanel;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private List<CardData> _cards;

    private List<Weapon> _choosedWeapons = new();
    private List<CardData> _choosedCards = new();
    private int _countWeapons = 5;
    private int _countCards = 10;

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