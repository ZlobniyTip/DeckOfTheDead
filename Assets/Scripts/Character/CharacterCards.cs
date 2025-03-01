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
        private readonly List<CardData> Ñards = new();

        [SerializeField] private SaverTest _saver;

        public List<CardData> Cards => Ñards;

        public event Action<List<CardData>> Initialized;

        public void AddCard(CardData card, Action equipmentChanged)
        {
            Ñards.Add(card);
            equipmentChanged?.Invoke();
        }

        public void InitializeCard(List<CardData> cards)
        {
            Ñards.Clear();

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].State.Status == ItemStatus.Purchased)
                    Ñards.Add(cards[i]);
            }

            Initialized?.Invoke(Ñards);
        }
    }
}