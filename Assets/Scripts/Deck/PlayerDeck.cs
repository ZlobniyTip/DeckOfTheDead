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
        private readonly HashSet<CardData> UsedCards = new();
        private readonly bool ChecksActivity = true;
        private readonly List<CardView> PlayerCards = new();

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
            _characterCards.Initialized += TakeStartCards;
            _selectedCard.ChosenCards += TakeSelectedCards;
        }

        private void OnDestroy()
        {
            _characterCards.Initialized -= TakeStartCards;
            _selectedCard.ChosenCards -= TakeSelectedCards;
        }

        public void TakeSelectedCards(List<CardData> cards)
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

        private void TakeStartCards(List<CardData> cards)
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
            while (PlayerCards.Count < 5)
            {
                CreateCard();
            }

            StartCoroutine(ViewActivity());
        }

        private IEnumerator ViewActivity()
        {
            var delay = new WaitForSeconds(0.3f);

            while (ChecksActivity)
            {
                foreach (var card in PlayerCards)
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
            if (PlayerCards.Contains(cardView))
            {
                UsedCards.Remove(cardView.Card);
                PlayerCards.Remove(cardView);
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
                PlayerCards.Add(cardView);

                CardViewUnit cardViewUnit = cardView as CardViewUnit;
                _leanHelper.ChangedLanguage += cardViewUnit.TransferData;
            }

            if (randomCardData is CardDataWeapon)
            {
                CardView cardView = Instantiate(_cardViewWeapon, _ñontainer);
                cardView.Initialize(randomCardData);
                PlayerCards.Add(cardView);

                CardViewWeapon cardViewWeapon = cardView as CardViewWeapon;
                _leanHelper.ChangedLanguage += cardViewWeapon.TransferData;
            }
        }

        private CardData GetUniqueCard()
        {
            if (PlayerCards.Count >= _selectedCards.Count)
                return null;

            CardData randomCard;

            do
            {
                randomCard = _selectedCards[Random.Range(0, _selectedCards.Count)];
            } 
            while 
            (UsedCards.Contains(randomCard));

            UsedCards.Add(randomCard);
            return randomCard;
        }
    }
}