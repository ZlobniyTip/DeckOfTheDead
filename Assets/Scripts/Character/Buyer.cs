using System;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private List<RangeWeapon> _rangeWeapons;
    [SerializeField] private List<MelleWeapon> _melleWeapons;
    [SerializeField] private List<CardData> _cards;

    [SerializeField] private CharacterShooting _characterShooting;
    [SerializeField] private CharacterCards _characterCards;

    private int _money;

    public event Action<int> MoneyChanged;
    public event Action EquipmentChanged;

    public int Money => _money;
    public Character Character => _character;

#if UNITY_EDITOR
    private void Start()
    {
        _money = 100000;
        MoneyChanged?.Invoke(_money);
    }
#endif

    public void GetMoney(int money)
    {
        _money += money;
        MoneyChanged?.Invoke(_money);
    }

    public void LoadMoney(int money)
    {
        _money = money;
        MoneyChanged?.Invoke(_money);
    }

    public bool TryBuy(IProduct product)
    {
        if (_money < product.Price)
            return false;

        _money -= product.Price;
        product.State.SetStatus(ItemStatus.Purchased);

        MoneyChanged?.Invoke(_money);
        EquipmentChanged?.Invoke();

        return true;
    }

    public bool TryLevelUpCard(int price)
    {
        if (_money < price)
            return false;

        _money -= price;
        MoneyChanged?.Invoke(_money);
        EquipmentChanged?.Invoke();

        return true;
    }

    public void EquipItem(IProduct product)
    {
        switch (product.Type)
        {
            case ItemType.RangeWeapon:
                _characterShooting.EquipWeapon(_rangeWeapons[product.Index], EquipmentChanged);
                break;
            case ItemType.MelleWeapon:
                _characterShooting.EquipWeapon(_melleWeapons[product.Index], EquipmentChanged);
                break;
            case ItemType.Card:
                _characterCards.AddCard(_cards[product.Index], EquipmentChanged);
                break;
        }
    }
}