using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Buyer _buyer;

    [SerializeField] private ItemType _type;

    [SerializeField] private List<Weapon> _rangeWeapon;
    [SerializeField] private List<Weapon> _melleWeapon;
    [SerializeField] private List<CardDataUnit> _cardsUnit;
    [SerializeField] private List<CardDataWeapon> _cardsWeapon;

    [SerializeField] private ItemView _template;
    [SerializeField] private CardView _templateCardWeapon;
    [SerializeField] private CardView _templateCardUnit;
    [SerializeField] private GameObject _itemContainer;

    private readonly List<ItemView> _content = new();

    public event Action PlayerEquippedItem;

    private void OnEnable()
    {
        switch (_type)
        {
            case ItemType.RangeWeapon:
                foreach (var item in _rangeWeapon)
                    AddItemView(item);
                break;
            case ItemType.MelleWeapon:
                foreach (var item in _melleWeapon)
                    AddItemView(item);
                break;
            case ItemType.Card:
                DeterminTypeCard();
                break;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _content)
        {
            item.PurchaseButtonPressed -= OnPurchaseButtonPressed;
            item.EquipButtonPressed -= OnEquipButtonPressed;
            Destroy(item.gameObject);
        }

        _content.Clear();
    }

    private void DeterminTypeCard()
    {
        if (_cardsUnit.Count > 0)
        {
            foreach (var item in _cardsUnit)
            {
                AddCardView(item, item);
            }
        }

        if (_cardsWeapon.Count > 0)
        {
            foreach (var item in _cardsWeapon)
            {
                AddCardView(item, item);
            }
        }
    }

    private void AddItemView(IProduct product)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.Init(product, this);
        view.PurchaseButtonPressed += OnPurchaseButtonPressed;
        view.EquipButtonPressed += OnEquipButtonPressed;
        _content.Add(view);
    }

    private void AddCardView(IProduct product, CardData card)
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
            view.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            view.Init(product, this);
            view.Initialize(card);
            view.SwitchDragAndDrop(false);
            view.PurchaseButtonPressed += OnPurchaseButtonPressed;
            view.EquipButtonPressed += OnEquipButtonPressed;
            _content.Add(view);
        }
    }

    private void OnPurchaseButtonPressed(ItemView view)
    {
        _buyer.TryBuy(view.Product);
    }

    private void OnEquipButtonPressed(ItemView view)
    {
        PlayerEquippedItem?.Invoke();
        _buyer.EquipItem(view.Product);
    }
}