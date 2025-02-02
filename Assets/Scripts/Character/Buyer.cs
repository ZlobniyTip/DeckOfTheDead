using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterShooting))]
[RequireComponent(typeof(CharacterCards))]
[RequireComponent(typeof(Character))]
public class Buyer : MonoBehaviour
{
    [SerializeField] private List<RangeWeapon> _rangeWeapons;
    [SerializeField] private List<MelleWeapon> _melleWeapons;
    [SerializeField] private List<CardData> _cards;

    private Character _character;
    private CharacterShooting _characterShooting;
    private CharacterCards _characterCards;

    private int _money;

    public event Action<int> MoneyChanged;
    public event Action EquipmentChanged;
    public event Action Initialized;

    public int Money => _money;
    public Character Character => _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _characterCards = GetComponent<CharacterCards>();
        _characterShooting = GetComponent<CharacterShooting>();
    }

    private void Start()
    {
        _money = 1000;
        Initialized?.Invoke();
    }

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