using DragAndDrop;
using Lean.Localization;
using System;
using TMPro;
using UI.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public abstract class CardView : ItemView
    {
        [SerializeField] private Image _activity;
        [SerializeField] private TMP_Text _levelPrice;

        [SerializeField] private Button _selectedButton;
        [SerializeField] private TMP_Text _selectButtonText;

        protected CardData _cardData;
        private DragAndDropCardUnit _dragAndDrop;
        private DragAndDropCardWeapon _dragAndDropWeapon;

        public CardData Card => _cardData;

        public event Action<CardView> LevelUpButtonPressed;
        public event Action<CardView> SelectedCard;

        private void Awake()
        {
            _dragAndDrop = GetComponent<DragAndDropCardUnit>();
            _dragAndDropWeapon = GetComponent<DragAndDropCardWeapon>();

            _equipButton.onClick.AddListener(OnLevelUpPressed);
            _selectedButton.onClick.AddListener(OnSelectedCard);
        }

        public abstract void Initialize(CardData cardData);

        public void SetInteractable(bool isInteractable)
        {
            _selectedButton.interactable = isInteractable;
        }

        public void SetSelectedStatus(CardStatus status)
        {
            if (status == CardStatus.Selected)
            {
                _cardData.State.SetSelectedStatus(CardStatus.Selected);
                _selectedButton.GetComponent<Image>().color = Color.green;
            }
            else
            {
                _cardData.State.SetSelectedStatus(CardStatus.NotSelected);
                _selectedButton.GetComponent<Image>().color = Color.black;
            }
        }

        public void SelectedButtonLock(CardStatus status)
        {
            SetSelectedStatus(status);

            if (status == CardStatus.Selected)
            {
                _selectButtonText.text = LeanLocalization.GetTranslationText("Selected");
            }
            else
            {
                _selectButtonText.text = LeanLocalization.GetTranslationText("Not selected");
            }
        }

        public void ActivateSelectedButton()
        {
            _selectedButton.gameObject.SetActive(true);
        }

        public void ActivateCard()
        {
            _activity.gameObject.SetActive(false);
            SwitchDragAndDrop(true);
        }

        public void DeactivateCard()
        {
            _activity.gameObject.SetActive(true);
            SwitchDragAndDrop(false);
        }

        public void SwitchDragAndDrop(bool isActiv)
        {
            if (_dragAndDrop != null)
                _dragAndDrop.enabled = isActiv;

            if (_dragAndDropWeapon != null)
                _dragAndDropWeapon.enabled = isActiv;
        }

        public void DeterminPriceLevelUp()
        {
            switch (_cardData.Level)
            {
                case 0:
                    _levelPrice.text = _cardData.PriceLevel1.ToString();
                    break;

                case 1:
                    _levelPrice.text = _cardData.PriceLevel2.ToString();
                    break;

                case 2:
                    _levelPrice.text = _cardData.PriceLevel3.ToString();
                    break;

                default:
                    _levelPrice.text = LeanLocalization.GetTranslationText("MaxLevel");
                    break;
            }
        }

        public void ShowSelectedButtonText()
        {
            _selectButtonText.text = LeanLocalization.GetTranslationText("Select");
        }

        private void OnLevelUpPressed()
        {
            LevelUpButtonPressed?.Invoke(this);
        }

        private void OnSelectedCard()
        {
            SelectedCard?.Invoke(this);
        }
    }
}