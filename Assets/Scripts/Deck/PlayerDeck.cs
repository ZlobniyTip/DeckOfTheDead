using System.Collections;
using System.Collections.Generic;
using Card;
using Character;
using SDK;
using UnityEngine;

namespace Deck
{
    public class PlayerDeck : MonoBehaviour
    {
        private readonly HashSet<CardData> _usedCards = new();
        private readonly List<CardView> _playerCards = new();
        private readonly int _numberCardsHand = 5;

        [SerializeField] private Player _character;
        [SerializeField] private CharacterCards _characterCards;
        [SerializeField] private Transform _ñontainer;
        [SerializeField] private CardView _cardViewUnit;
        [SerializeField] private CardView _cardViewWeapon;
        [SerializeField] private PlayerEnergy _playerEnergy;
        [SerializeField] private LeanHelper _leanHelper;
        [SerializeField] private SelectedCard _selectedCard;

        private List<CardData> _selectedCards = new();

        public Player Character => _character;

        private void Awake()
        {
            _characterCards.Initialized += OnTakeStartCards;
            _selectedCard.CardsChosed += OnTakeSelectedCards;
        }

        private void OnDestroy()
        {
            _characterCards.Initialized -= OnTakeStartCards;
            _selectedCard.CardsChosed -= OnTakeSelectedCards;
        }

        public void OnTakeSelectedCards(List<CardData> cards)
        {
            if (_selectedCards.Count != 0)
            {
                foreach (var card in _selectedCards)
                {
                    Destroy(card);
                }
            }

            _selectedCards.Clear();
            _selectedCards = new List<CardData>(cards);
            TakeCards();
        }

        private void OnTakeStartCards(List<CardData> cards)
        {
            _selectedCards.Clear();

            foreach (var card in cards)
            {
                if (card.State.SelectedStatus == CardStatus.Selected)
                    _selectedCards.Add(card);
            }

            TakeCards();
        }

        private void TakeCards()
        {
            while (_playerCards.Count < _numberCardsHand)
            {
                CreateCard();
            }

            StartCoroutine(ViewActivity());
        }

        private IEnumerator ViewActivity()
        {
            float amountDelay = 0.3f;
            var delay = new WaitForSeconds(amountDelay);

            while (enabled)
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
            CardView cardView = null;

            if (randomCardData is CardDataUnit)
            {
                cardView = Instantiate(_cardViewUnit, _ñontainer);
            }
            else if (randomCardData is CardDataWeapon)
            {
                cardView = Instantiate(_cardViewWeapon, _ñontainer);
            }

            if (cardView != null)
            {
                cardView.Initialize(randomCardData);
                _playerCards.Add(cardView);

                if (cardView is CardViewUnit unit)
                {
                    _leanHelper.LanguageChanged += unit.TransferData;
                }
                else if (cardView is CardViewWeapon weapon)
                {
                    _leanHelper.LanguageChanged += weapon.TransferData;
                }
            }
        }

        private CardData GetUniqueCard()
        {
            if (_playerCards.Count >= _selectedCards.Count)
                return null;

            CardData randomCard;

            do
            {
                randomCard = _selectedCards[Random.Range(0, _selectedCards.Count)];
            } 
            while 
            (_usedCards.Contains(randomCard));

            _usedCards.Add(randomCard);
            return randomCard;
        }
    }
}