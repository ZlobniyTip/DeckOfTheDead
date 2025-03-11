using System;
using System.Collections.Generic;
using Card;
using Other;
using Save;
using UnityEngine;
using Weapons;

namespace Character
{
    [RequireComponent(typeof(CharacterShooting))]
    [RequireComponent(typeof(CharacterCards))]
    [RequireComponent(typeof(Player))]
    public class Buyer : MonoBehaviour
    {
        [SerializeField] private List<RangeWeapon> _rangeWeapons;
        [SerializeField] private List<MelleWeapon> _melleWeapons;
        [SerializeField] private List<CardData> _cards;

        private Player _character;
        private CharacterShooting _characterShooting;
        private CharacterCards _characterCards;

        private int _money;

        public event Action<int> MoneyChanged;
        public event Action EquipmentChanged;

        public int Money => _money;
        public Player Character => _character;

        private void Awake()
        {
            _character = GetComponent<Player>();
            _characterCards = GetComponent<CharacterCards>();
            _characterShooting = GetComponent<CharacterShooting>();
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

        public void TryBuy(IProduct product)
        {
            if (SpendMoney(product.Price))
            {
                product.State.SetStatus(ItemStatus.Purchased);

                if (product.Type == ItemType.Card)
                    EquipItem(product);

                EquipmentChanged?.Invoke();
            }
        }

        public bool TryLevelUpCard(int price)
        {
            return SpendMoney(price);
        }

        private bool SpendMoney(int amount)
        {
            if (_money < amount)
                return false;

            _money -= amount;
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
}
