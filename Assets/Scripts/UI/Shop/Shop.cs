using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Buyer _buyer;

    [SerializeField] private ItemType _type;

    [SerializeField] private List<RangeWeapon> _rangeWeapon;
    [SerializeField] private List<MelleWeapon> _melleWeapon;
    [SerializeField] private List<Card> _cards;

    [SerializeField] private ItemView _template;
    [SerializeField] private GameObject _itemContainer;

    private readonly List<ItemView> _content = new();

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

    private void AddItemView(Product product)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.Init(product);
        view.PurchaseButtonPressed += OnPurchaseButtonPressed;
        view.EquipButtonPressed += OnEquipButtonPressed;
        _content.Add(view);
    }

    private void OnPurchaseButtonPressed(ItemView view)
    {
        _buyer.TryBuy(view.Product);
    }

    private void OnEquipButtonPressed(ItemView view)
    {
        _buyer.EquipItem(view.Product);
    }
}
