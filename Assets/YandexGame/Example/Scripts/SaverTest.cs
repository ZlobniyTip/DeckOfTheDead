using Card;
using Character;
using Deck;
using Save;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons;

namespace YG.Example
{
    public class SaverTest : MonoBehaviour
    {
        [SerializeField] private Buyer _buyer;

        [SerializeField] private List<Weapon> _rangeWeapons;
        [SerializeField] private List<Weapon> _melleWeapons;
        [SerializeField] private List<CardData> _cards;
        [SerializeField] private CharacterCards _characterCards;
        [SerializeField] private SelectedCard _selectedCard;

        private Weapon _currentWeapon;
        private Weapon _currentMelleWeapon;
        private int _emptyParameter = 0;

        public event Action<int> LoadedLeaderboardScore;
        public event Action<int> SavedLeaderboardScore;

        private void Start()
        {
            if (YandexGame.SDKEnabled == true)
                GetLoad();
        }

        private void OnEnable()
        {
            if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                YandexGame.savesData.indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
            }

            YandexGame.GetDataEvent += GetLoad;
            _selectedCard.SelectedCardsSave += Save;
            _buyer.EquipmentChanged += Save;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent += GetLoad;
        }

        private void OnDestroy()
        {
            _selectedCard.SelectedCardsSave -= Save;
            _buyer.EquipmentChanged -= Save;
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
                YandexGame.savesData.cardLevel[i] = _cards[i].State.Level;
                YandexGame.savesData.cardStatuses[i] = _cards[i].State.SelectedStatus;
            }

            YandexGame.savesData.playerMoney = _buyer.Money;
            YandexGame.savesData.leaderboardScore = _buyer.Character.LeaderboardScore;
            SavedLeaderboardScore?.Invoke(YandexGame.savesData.leaderboardScore);

            YandexGame.SaveProgress();
        }

        public void GetLoad()
        {
            for (int i = 0; i < _melleWeapons.Count; i++)
            {
                _melleWeapons[i].Init(YandexGame.savesData.melleWeaponStates[i], _emptyParameter);

                if (_melleWeapons[i].State.Status == ItemStatus.Equipped)
                    _currentMelleWeapon = _melleWeapons[i];
            }

            for (int i = 0; i < _rangeWeapons.Count; i++)
            {
                _rangeWeapons[i].Init(YandexGame.savesData.rangeWeaponStates[i], _emptyParameter);

                if (_rangeWeapons[i].State.Status == ItemStatus.Equipped)
                {
                    _currentWeapon = _rangeWeapons[i];

                    if (_currentMelleWeapon != null)
                        _currentMelleWeapon.State.SetStatus(ItemStatus.Purchased);
                }
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].Init(YandexGame.savesData.cardStates[i], YandexGame.savesData.cardLevel[i]);
                _cards[i].InitCardStatus(YandexGame.savesData.cardStatuses[i]);
            }

            _characterCards.InitializeCard(_cards);
            _buyer.LoadMoney(YandexGame.savesData.playerMoney);
            _buyer.Character.LoadScore(YandexGame.savesData.leaderboardScore);

            AudioListener.volume = YandexGame.savesData.sound;

            if (_currentWeapon != null)
            {
                _buyer.EquipItem(_currentWeapon);
            }
            else if (_currentMelleWeapon != null)
            {
                _buyer.EquipItem(_currentMelleWeapon);
            }

            LoadedLeaderboardScore?.Invoke(_buyer.Character.LeaderboardScore);

            Save();
        }

        public void ResetSave()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
    }
}