using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCards : MonoBehaviour
{
    [SerializeField] private List<CardData> _cards;

    public void AddCard(CardData card, Action equipmentChanged)
    {
        _cards.Add(card);
        equipmentChanged?.Invoke();
    }
}