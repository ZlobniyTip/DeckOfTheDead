using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardView : ItemView
{
    [SerializeField] private Image _activity;
    [SerializeField] private TMP_Text _levelPrice;

    protected CardData _cardData;
    private DragAndDropCardUnit _dragAndDrop;
    private DragAndDropCardWeapon _dragAndDropWeapon;

    public CardData Card => _cardData;

    public event Action<CardView> LevelUpButtonPressed;

    private void Awake()
    {
        _dragAndDrop = GetComponent<DragAndDropCardUnit>();
        _dragAndDropWeapon = GetComponent<DragAndDropCardWeapon>();

        _equipButton.onClick.AddListener(OnLevelUpPressed);
    }

    public abstract void Initialize(CardData cardData);

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
                _levelPrice.text = "Max Level";
                break;
        }
    }

    private void OnLevelUpPressed()
    {
        LevelUpButtonPressed?.Invoke(this);
    }
}
