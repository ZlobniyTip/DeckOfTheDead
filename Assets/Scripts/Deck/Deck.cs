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
    private HashSet<CardData> _usedCards = new();
    private bool _checksActivity = true;

    public Character Character => _character;

    private void Awake()
    {
        _characterCards.Initialized += TakeCards;
    }

    private void OnDisable()
    {
        _characterCards.Initialized -= TakeCards;
    }

    private void TakeCards()
    {
        Debug.Log("1");
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
        CardData randomCardUnit;

        do
        {
            randomCardUnit = _characterCards.Cards[Random.Range(0, _characterCards.Cards.Count)];
        } while (_usedCards.Contains(randomCardUnit));

        _usedCards.Add(randomCardUnit);
        return randomCardUnit;
    }
}