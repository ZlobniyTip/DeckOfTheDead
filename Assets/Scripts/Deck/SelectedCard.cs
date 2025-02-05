using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCard : MonoBehaviour
{
    [SerializeField] private CharacterCards _cards;
    [SerializeField] private Deck _deck;

    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private CardViewUnit _templateCardUnit;
    [SerializeField] private CardViewWeapon _templateCardWeapon;

    [SerializeField] private GameObject _autoChoiceButton;
    [SerializeField] private GameObject _selectedButton;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private GameObject _panel;

    private List<CardView> _content = new();
    private List<CardView> _selectedCards = new();

    private void Awake()
    {
        _cards.InitializedCards += FillDeck;
    }

    private void OnDestroy()
    {
        foreach (var card in _content)
        {
            Destroy(card);
        }
    }

    public void StartGame()
    {
        _deck.TakeSelectedCards(_selectedCards);
        _panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void AutomaticallySelectCards()
    {
        int maxCountCards = 10;

        for (int i = 0; i < maxCountCards; i++)
        {
            OnSelectedCard(_content[i]);
        }
    }

    private void FillDeck(List<CardData> cards)
    {
        Time.timeScale = 0;

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
                _content.Add(view);
            }
        }
    }

    private void OnSelectedCard(CardView card)
    {
        _selectedCards.Add(card);
        card.SelectedButtonLock();

        if (_selectedCards.Count >= 10)
        {
            _selectedButton.SetActive(true);

            foreach (var button in _content)
            {
                button.SelectedButtonLock();
            }
        }

        _autoChoiceButton.SetActive(false);
    }
}