using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Card
{
    public class CardViewUnit : CardView
    {
        [SerializeField] private TMP_Text _energy;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _ability;

        private CardDataUnit _cardDataUnit;

        public override void Initialize(CardData cardData)
        {
            CardData = cardData;
            CardDataUnit cardDataUnit = CardData as CardDataUnit;
            _cardDataUnit = cardDataUnit;

            Icon.sprite = cardDataUnit.Icon;
            Name.text = LeanLocalization.GetTranslationText(cardDataUnit.Name);
            _energy.text = cardDataUnit.Energy.ToString();
            _level.text = cardDataUnit.Level.ToString();
            _health.text = cardDataUnit.UnitHealth.ToString();
            _damage.text = cardDataUnit.Damage.ToString();
            _ability.text = LeanLocalization.GetTranslationText(cardDataUnit.Ability);
        }

        public void TransferData()
        {
            Name.text = LeanLocalization.GetTranslationText(_cardDataUnit.Name);
            _ability.text = LeanLocalization.GetTranslationText(_cardDataUnit.Ability);
        }

        public void UpdateLevelText(int level)
        {
            _level.text = level.ToString();
        }
    }
}