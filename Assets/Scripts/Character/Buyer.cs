using System;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    [SerializeField] private List<RangeWeapon> _rangeWeapons;
    [SerializeField] private List<MelleWeapon> _melleWeapons;
    [SerializeField] private List<Card> _cards;

    [SerializeField] private CharacterShooting _characterShooting;
    [SerializeField] private CharacterCards _characterCards;

    private int _money;

    public event Action<int> MoneyChanged;
    public event Action EquipmentChanged;

#if UNITY_EDITOR
    private void Start()
    {
        _money += 9000;
    }
#endif

    public bool TryBuy(Product product)
    {
        if (_money < product.Price)
            return false;

        _money -= product.Price;
        MoneyChanged?.Invoke(_money);
        product.State.SetStatus(ItemStatus.Purchased);
        EquipmentChanged?.Invoke();

        return true;
    }

    public void EquipItem(Product product)
    {
        switch (product.Type)
        {
            case ItemType.RangeWeapon:
                _characterShooting.EquipWeapon(_rangeWeapons[product.Index]);
                break;
            case ItemType.MelleWeapon:
                _characterShooting.EquipWeapon(_melleWeapons[product.Index]);
                break;
            case ItemType.Card:
                _characterCards.AddCard(_cards[product.Index]);
                break;
        }

        EquipmentChanged?.Invoke();
    }
}