using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxView : MonoBehaviour
{
    [SerializeField] private LootBox _lootBox;
    [SerializeField] private List<Image> _images;

    private void OnEnable()
    {
        ShowLootBoxItems();
    }

    private void ShowLootBoxItems()
    {
        for (int i = 0; i < _lootBox.Weapons.Count; i++)
        {
            _images[i].sprite = _lootBox.Weapons[i].Icon;
        }

        for (int i = 0; i < _lootBox.Cards.Count; i++)
        {
            _images[i].sprite = _lootBox.Cards[i].Icon;
        }
    }
}