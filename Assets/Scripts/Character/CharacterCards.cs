using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCards : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;

    public void AddCard(Card card, Action equipmentChanged)
    {
        _cards.Add(card);
        equipmentChanged?.Invoke();
    }
}