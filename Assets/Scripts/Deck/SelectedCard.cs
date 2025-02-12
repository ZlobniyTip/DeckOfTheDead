using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCard : MonoBehaviour
{
    [SerializeField] private CharacterCards _character;
    [SerializeField] private Deck _deck;

    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private CardViewUnit _templateCardUnit;
    [SerializeField] private CardViewWeapon _templateCardWeapon;

    [SerializeField] private Button _selectButton;

    private List<CardView> _content = new();
    private List<CardData> _selectedCards = new();

    public event Action<List<CardData>> SelectedCards;
    public event Action SelectedCardsSave;

    private void OnEnable()
    {
        FillDeck(_character.Cards);
    }

    private void OnDisable()
    {
        SelectedCards?.Invoke(_selectedCards);
        SelectedCardsSave?.Invoke();

        foreach (var card in _content)
        {
            card.SelectedCard -= OnSelectedCard;
            Destroy(card.gameObject);
        }
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
                _content.Add(view);

                if (view.Card.State.SelectedStatus == CardStatus.NotSelected)
                {
                    view.SetInteractable(false);
                }
                else
                {
                    _selectedCards.Add(view.Card);
                }
            }
        }
    }

    private void OnSelectedCard(CardView card)
    {
        if (card.Card.State.SelectedStatus == CardStatus.NotSelected)
        {
            card.SelectedButtonLock(CardStatus.Selected);
            _selectedCards.Add(card.Card);
        }
        else
        {
            card.SelectedButtonLock(CardStatus.NotSelected);
            _selectedCards.Remove(card.Card);
        }

        if (_selectedCards.Count >= 10)
        {
            foreach (var button in _content)
            {
                if (button.Card.State.SelectedStatus == CardStatus.NotSelected)
                    button.SetInteractable(false);
            }

            _selectButton.interactable = true;
        }
        else
        {
            foreach (var button in _content)
            {
                button.SetInteractable(true);
            }

            _selectButton.interactable = false;
        }
    }
}