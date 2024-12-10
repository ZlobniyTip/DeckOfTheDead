using System;
using System.Collections.Generic;
using UnityEngine;

namespace YG.Example
{
    public class SaverTest : MonoBehaviour
    {
        [SerializeField] private Buyer _buyer;

        [SerializeField] private List<Weapon> _rangeWeapons;
        [SerializeField] private List<Weapon> _melleWeapons;
        [SerializeField] private List<Card> _cards;

        private Weapon _currentWeapon;

        public event Action<int> LoadedData;

        private void OnEnable()
        {
            YandexGame.GetDataEvent += GetLoad;
            _buyer.EquipmentChanged += Save;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= GetLoad;
            _buyer.EquipmentChanged -= Save;
        }

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                GetLoad();
        }

        public void Save()
        {
            for (int i = 0; i < _melleWeapons.Count; i++)
            {
                YandexGame.savesData.melleWeaponStates[i] = _melleWeapons[i].State.Status;
            }

            for (int i = 0; i < _rangeWeapons.Count; i++)
            {
                YandexGame.savesData.rangeWeaponStates[i] = _rangeWeapons[i].State.Status;
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                YandexGame.savesData.cardStates[i] = _cards[i].State.Status;
            }

            YandexGame.savesData.playerMoney = _buyer.Money;
            YandexGame.savesData.leaderboardScore = _buyer.CharacterShooting.Score;

            YandexGame.SaveProgress();
        }

        public void GetLoad()
        {
            for (int i = 0; i < _melleWeapons.Count; i++)
            {
                _melleWeapons[i].Init(YandexGame.savesData.melleWeaponStates[i]);

                if (_melleWeapons[i].State.Status == ItemStatus.Equipped)
                    _currentWeapon = _melleWeapons[i];
            }

            for (int i = 0; i < _rangeWeapons.Count; i++)
            {
                _rangeWeapons[i].Init(YandexGame.savesData.rangeWeaponStates[i]);

                if (_rangeWeapons[i].State.Status == ItemStatus.Equipped)
                    _currentWeapon = _rangeWeapons[i];
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].Init(YandexGame.savesData.cardStates[i]);
            }

            _buyer.LoadMoney(YandexGame.savesData.playerMoney);
            _buyer.CharacterShooting.LoadScore(YandexGame.savesData.leaderboardScore);

            if (_currentWeapon != null)
            _buyer.EquipItem(_currentWeapon);

            LoadedData?.Invoke(_buyer.CharacterShooting.Score);
        }
    }
}