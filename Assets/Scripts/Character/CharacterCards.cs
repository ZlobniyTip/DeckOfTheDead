using System.Collections.Generic;
using UnityEngine;

public class CharacterCards : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }
}