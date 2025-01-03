using TMPro;
using UnityEngine;

public class CardViewUnit : CardView
{
    [SerializeField] private TMP_Text _energy;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _ability;

    public override void Initialize(CardData cardData)
    {
        _cardData = cardData;
        CardDataUnit cardDataUnit = _cardData as CardDataUnit;

        _icon.sprite = cardDataUnit.Icon;
        _name.text = cardDataUnit.Name;
        _energy.text = cardDataUnit.Energy.ToString();
        _level.text = cardDataUnit.Level.ToString();
        _health.text = cardDataUnit.UnitHealth.ToString();
        _damage.text = cardDataUnit.Damage.ToString();
        _ability.text = cardDataUnit.Ability;
    }
}
