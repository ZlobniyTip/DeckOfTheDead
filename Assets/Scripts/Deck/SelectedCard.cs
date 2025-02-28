using Card;
using Character;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deck
{
    public class SelectedCard : MonoBehaviour
    {
        private readonly List<CardView> Content = new();
        private readonly List<CardData> SelectedCards = new();

        [SerializeField] private CharacterCards _character;
        [SerializeField] private PlayerDeck _deck;

        [SerializeField] private GameObject _itemContainer;
        [SerializeField] private CardViewUnit _templateCardUnit;
        [SerializeField] private CardViewWeapon _templateCardWeapon;

        [SerializeField] private Button _selectButton;

        public event Action<List<CardData>> ChosenCards;
        public event Action SelectedCardsSave;

        private void OnEnable()
        {
            SelectedCards.Clear();
            FillDeck(_character.Cards);
        }

        private void OnDisable()
        {
            ChosenCards?.Invoke(SelectedCards);
            SelectedCardsSave?.Invoke();

            foreach (var card in Content)
            {
                card.SelectedCard -= OnSelectedCard;
                Destroy(card.gameObject);
            }

            Content.Clear();
        }

        private void FillDeck(List<CardData> cards)
        {
            foreach (var card in cards)
            {
                if (card is CardDataUnit)
                {
                    var view = Instantiate(_templateCardUnit, _itemContainer.transform);
                    Init(view);
                }
                else if (card is CardDataWeapon)
                {
                    var view = Instantiate(_templateCardWeapon, _itemContainer.transform);
                    Init(view);
                }

                void Init(CardView view)
                {
                    view.gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    view.Initialize(card);
                    view.SwitchDragAndDrop(false);
                    view.ActivateSelectedButton();
                    view.SelectedCard += OnSelectedCard;
                    view.ShowSelectedButtonText();
                    view.SetSelectedStatus(view.Card.State.SelectedStatus);
                    Content.Add(view);

                    if (view.Card.State.SelectedStatus == CardStatus.NotSelected)
                    {
                        view.SetInteractable(false);
                    }
                    else
                    {
                        SelectedCards.Add(view.Card);
                    }
                }
            }
        }

        private void OnSelectedCard(CardView card)
        {
            if (card.Card.State.SelectedStatus == CardStatus.NotSelected)
            {
                card.SelectedButtonLock(CardStatus.Selected);
                SelectedCards.Add(card.Card);
            }
            else
            {
                card.SelectedButtonLock(CardStatus.NotSelected);
                SelectedCards.Remove(card.Card);
            }

            if (SelectedCards.Count >= 10)
            {
                foreach (var button in Content)
                {
                    if (button.Card.State.SelectedStatus == CardStatus.NotSelected)
                        button.SetInteractable(false);
                }

                _selectButton.interactable = true;
            }
            else
            {
                foreach (var button in Content)
                {
                    button.SetInteractable(true);
                }

                _selectButton.interactable = false;
            }
        }
    }
}