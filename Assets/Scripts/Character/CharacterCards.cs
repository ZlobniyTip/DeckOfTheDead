using System;
using System.Collections.Generic;
using Card;
using Save;
using UnityEngine;
using YG.Example;

namespace Character
{
    public class CharacterCards : MonoBehaviour
    {
        private readonly List<CardData> _purchasedCards = new ();

        [SerializeField] private SaverTest _saver;

        public event Action<List<CardData>> Initialized;

        public List<CardData> Cards => _purchasedCards;

        public void AddCard(CardData card, Action equipmentChanged)
        {
            _purchasedCards.Add(card);
            equipmentChanged?.Invoke();
        }

        public void InitializeCard(List<CardData> cards)
        {
            _purchasedCards.Clear();

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].State.Status == ItemStatus.Purchased)
                    _purchasedCards.Add(cards[i]);
            }

            Initialized?.Invoke(_purchasedCards);
        }
    }
}