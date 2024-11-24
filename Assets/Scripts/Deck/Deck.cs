using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private List<Card> _cards;
    [SerializeField] private Transform _ñontainer;
    [SerializeField] private CardView _cardView;

    private List<CardView> _playerCards;

    public Character Character => _character;

    private void Start()
    {
        _playerCards = new List<CardView>();
        HashSet<Card> selectedCards = new HashSet<Card>();

        while (_playerCards.Count < 5)
        {
            Card randomCard = _cards[Random.Range(0, _cards.Count)];

            while (selectedCards.Contains(randomCard))
            {
                randomCard = _cards[Random.Range(0, _cards.Count)];
            }

            selectedCards.Add(randomCard);

            CardView cardView = Instantiate(_cardView, _ñontainer);
            cardView.Initialized(randomCard);
            _playerCards.Add(cardView);
        }
    }
}
