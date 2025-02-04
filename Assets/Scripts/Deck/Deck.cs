using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterCards _characterCards;
    [SerializeField] private Transform _ñontainer;
    [SerializeField] private CardView _cardViewUnit;
    [SerializeField] private CardView _cardViewWeapon;
    [SerializeField] private PlayerEnergy _playerEnergy;
    [SerializeField] private LeanHelper _leanHelper;

    private List<CardView> _playerCards = new();
    private List<CardView> _selectedCards = new(); 
    private HashSet<CardData> _usedCards = new();
    private bool _checksActivity = true;

    public Character Character => _character;

    public void TakeSelectedCards(List<CardView> cardViews)
    {
        _selectedCards = new List<CardView>(cardViews); 
        TakeCards();
    }

    private void TakeCards()
    {
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

    public void TakeAwayPlayerEnergy(int energy)
    {
        _playerEnergy.UseUpEnergy(energy);
    }

    private void CreateCard()
    {
        CardData randomCardData = GetUniqueCard();

        if (randomCardData is CardDataUnit)
        {
            CardView cardView = Instantiate(_cardViewUnit, _ñontainer);
            cardView.Initialize(randomCardData);
            _playerCards.Add(cardView);

            CardViewUnit cardViewUnit = cardView as CardViewUnit;
            _leanHelper.ChangedLanguage += cardViewUnit.TransferData;
        }

        if(randomCardData is CardDataWeapon)
        {
            CardView cardView = Instantiate(_cardViewWeapon, _ñontainer);
            cardView.Initialize(randomCardData);
            _playerCards.Add(cardView);

            CardViewWeapon cardViewWeapon = cardView as CardViewWeapon;
            _leanHelper.ChangedLanguage += cardViewWeapon.TransferData;
        }
    }

    private CardData GetUniqueCard()
    {
        if (_playerCards.Count >= _selectedCards.Count)
            return null;

        CardData randomCard;

        do
        {
            randomCard = _selectedCards[Random.Range(0, _selectedCards.Count)].Card;
        } while (_usedCards.Contains(randomCard));

        _usedCards.Add(randomCard);
        return randomCard;
    }
}