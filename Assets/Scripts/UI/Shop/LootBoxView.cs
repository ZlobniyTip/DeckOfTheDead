using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class LootBoxView : MonoBehaviour
    {
        [SerializeField] private LootBox _lootBox;
        [SerializeField] private List<Image> _imagesCards;
        [SerializeField] private List<Image> _imagesWeapons;

        private void OnEnable()
        {
            ShowLootBoxItems();
        }

        private void ShowLootBoxItems()
        {
            for (int i = 0; i < _lootBox.Weapons.Count; i++)
            {
                _imagesWeapons[i].sprite = _lootBox.Weapons[i].Icon;
            }

            for (int i = 0; i < _lootBox.Cards.Count; i++)
            {
                _imagesCards[i].sprite = _lootBox.Cards[i].Icon;
            }
        }
    }
}