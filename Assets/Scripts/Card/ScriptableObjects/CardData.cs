using Other;
using Save;
using UnityEngine;

namespace Card
{
    public abstract class CardData : ScriptableObject, IProduct
    {
        private readonly int _damageOneLevel = 5;
        private readonly int damageTwoLevel = 10;
        private readonly int _damageThreeLevel = 20;

        private readonly int _healthOneLevel = 20;
        private readonly int _healthTwoLevel = 40;
        private readonly int _healthThreeLevel = 80;

        private const int LevelOne = 1;
        private const int LevelTwo = 2;
        private const int LevelThree = 3;

        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private int _energy;
        [SerializeField] private ItemType _type;
        [SerializeField] private int _index;
        [SerializeField] private int _price;
        [SerializeField] private int _priceLevel1;
        [SerializeField] private int _priceLevel2;
        [SerializeField] private int _priceLevel3;

        private ItemState _state = null;
        private int _level = 0;
        private int _bonusDamage = 0;
        private int _bonusHealth = 0;

        public int PriceLevel1 => _priceLevel1;

        public int PriceLevel2 => _priceLevel2;

        public int PriceLevel3 => _priceLevel3;

        public Sprite Icon => _icon;

        public string Name => _name;

        public int Energy => _energy;

        public int BonusDamage => _bonusDamage;

        public int BonusHealth => _bonusHealth;

        public int Level => _level;

        public ItemType Type => _type;

        public int Price => _price;

        public int Index => _index;

        public ItemState State => _state ??= new ItemState(ItemStatus.NotPurchased);

        public void Init(ItemStatus state, int level)
        {
            State.SetStatus(state);
            State.SetParameters(level);

            _level = State.Level;
            SetParametersFromLevel();
        }

        public void InitCardStatus(CardStatus cardStatus)
        {
            State.SetSelectedStatus(cardStatus);
        }

        public void SetParametersFromLevel()
        {
            switch (_level)
            {
                case LevelOne:
                    SetParameters(_damageOneLevel, _healthOneLevel);
                    break;

                case LevelTwo:
                    SetParameters(damageTwoLevel, _healthTwoLevel);
                    break;

                case LevelThree:
                    SetParameters(_damageThreeLevel, _healthThreeLevel);
                    break;
            }
        }

        private void SetParameters(int damage, int health)
        {
            _bonusDamage = damage;
            _bonusHealth = health;
        }
    }
}