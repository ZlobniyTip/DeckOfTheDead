using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Card
{
    public class CardViewWeapon : CardView
    {
        [SerializeField] private TMP_Text _energy;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _timeAction;
        [SerializeField] private TMP_Text _delayBetweenShots;

        private CardDataWeapon _cardDataWeapon;

        public override void Initialize(CardData cardData)
        {
            CardData = cardData;
            CardDataWeapon cardDataWeapon = CardData as CardDataWeapon;
            _cardDataWeapon = cardDataWeapon;

            Icon.sprite = cardDataWeapon.Icon;
            Name.text = LeanLocalization.GetTranslationText(cardDataWeapon.Name);

            if (Name.text == null)
                Name.text = cardDataWeapon.Name;

            _energy.text = cardDataWeapon.Energy.ToString();
            _level.text = cardDataWeapon.Level.ToString();
            _damage.text = cardDataWeapon.WeaponDamage.ToString();
            _delayBetweenShots.text = cardDataWeapon.DelayBetweenShots.ToString();
            _timeAction.text = LeanLocalization.GetTranslationText("Time of action") +
                $" {cardDataWeapon.TimeAction.ToString()} " + LeanLocalization.GetTranslationText("seconds");
        }

        public void TransferData()
        {
            Name.text = LeanLocalization.GetTranslationText(_cardDataWeapon.Name);

            if (Name.text == null)
                Name.text = _cardDataWeapon.Name;

            _timeAction.text = LeanLocalization.GetTranslationText("Time of action") +
                $" {_cardDataWeapon.TimeAction.ToString()} " + LeanLocalization.GetTranslationText("seconds");
        }

        public void UpdateLevelText(int level)
        {
            _level.text = level.ToString();
        }
    }
}