using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private List<Card> _cards;
    [SerializeField] private Transform _ñontainer;
    [SerializeField] private CardView _cardView;
    [SerializeField] private PlayerEnergy _playerEnergy;

    private List<CardView> _playerCards;
    private HashSet<Card> _usedCards;
    private bool _checksActivity = true;

    public Character Character => _character;

    private void Start()
    {
        _playerCards = new List<CardView>();
        _usedCards = new HashSet<Card>();

        while (_playerCards.Count < 5)
        {
            CreateCard();
        }

        StartCoroutine(ViewActivity());
    }

    private IEnumerator ViewActivity()
    {
        var delay = new WaitForSeconds(0.3f);

        while (_checksActivity)
        {
            foreach (var card in _playerCards)
            {
                if (card.Card.Energy <= _playerEnergy.CurrentEnergyCount)
                {
                    card.ActivateCard();
                }
                else
                {
                    card.DeactivateCard();
                }
            }

            yield return delay;
        }
    }

    public void RemoveCard(CardView cardView)
    {
        if (_playerCards.Contains(cardView))
        {
            _usedCards.Remove(cardView.Card);
            _playerCards.Remove(cardView);
        }

        CreateCard();
    }

    private void CreateCard()
    {
        Card randomCard = GetUniqueCard();

        CardView cardView = Instantiate(_cardView, _ñontainer);
        cardView.Initialized(randomCard);
        _playerCards.Add(cardView);
    }

    private Card GetUniqueCard()
    {
        Card randomCard;

        do
        {
            randomCard = _cards[Random.Range(0, _cards.Count)];
        } while (_usedCards.Contains(randomCard));

        _usedCards.Add(randomCard);
        return randomCard;
    }
}