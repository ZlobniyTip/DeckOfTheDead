using System;
using System.Collections.Generic;
using UnityEngine;
using YG.Example;

public class CharacterCards : MonoBehaviour
{
    [SerializeField] private SaverTest _saver;

    private List<CardData> _cards = new();

    public event Action<List<CardData>> Initialized;

    public List<CardData> Cards => _cards;

    public void AddCard(CardData card, Action equipmentChanged)
    {
        _cards.Add(card);
        equipmentChanged?.Invoke();
    }

    public void InitializeCard(List<CardData> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].State.Status == ItemStatus.Purchased)
                _cards.Add(cards[i]);
        }

        Initialized?.Invoke(_cards);
    }
}