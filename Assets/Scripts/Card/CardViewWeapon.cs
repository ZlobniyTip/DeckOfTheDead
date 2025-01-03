using TMPro;
using UnityEngine;

public class CardViewWeapon : CardView
{
    [SerializeField] private TMP_Text _energy;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _timeAction;
    [SerializeField] private TMP_Text _delayBetweenShots;

    public override void Initialize(CardData cardData)
    {
        _cardData = cardData;
        CardDataWeapon cardDataWeapon = _cardData as CardDataWeapon;

        _icon.sprite = cardDataWeapon.Icon;
        _name.text = cardDataWeapon.Name;
        _energy.text = cardDataWeapon.Energy.ToString();
        _level.text = cardDataWeapon.Level.ToString();
        _damage.text = cardDataWeapon.WeaponDamage.ToString();
        _delayBetweenShots.text = cardDataWeapon.DelayBetweenShots.ToString();
        _timeAction.text = $"Время действия {cardDataWeapon.TimeAction.ToString()} секунд";
    }
}