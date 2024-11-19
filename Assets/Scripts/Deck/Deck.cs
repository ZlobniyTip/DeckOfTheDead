using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private List<CardView> _cards;
    [SerializeField] private Transform _ñontainer;

    private List<CardView> _playerCards;

    public Character Character => _character;

    private void Start()
    {
        _playerCards = new List<CardView>();

        while (_playerCards.Count < 5)
        {
            CardView cardView = Instantiate(_cards[Random.Range(0, _cards.Count)], _ñontainer);
            _playerCards.Add(cardView);
        }
    }
}
