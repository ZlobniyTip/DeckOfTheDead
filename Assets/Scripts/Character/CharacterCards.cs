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
        private readonly List<CardData> PurchasedCards = new ();

        [SerializeField] private SaverTest _saver;

        public event Action<List<CardData>> Initialized;

        public List<CardData> Cards => PurchasedCards;

        public void AddCard(CardData card, Action equipmentChanged)
        {
            PurchasedCards.Add(card);
            equipmentChanged?.Invoke();
        }

        public void InitializeCard(List<CardData> cards)
        {
            PurchasedCards.Clear();

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].State.Status == ItemStatus.Purchased)
                    PurchasedCards.Add(cards[i]);
            }

            Initialized?.Invoke(PurchasedCards);
        }
    }
}