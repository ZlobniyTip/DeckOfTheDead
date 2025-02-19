using Units;
using Units.UnitConfi;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/Create new unit", order = 51)]
    public class CardDataUnit : CardData
    {
        [SerializeField] private Unit _prefabUnit;
        [SerializeField] private string _ability;

        public Unit PrefabUnit => _prefabUnit;
        public int UnitHealth => _prefabUnit.UnitConfig.Health;
        public int Damage => _prefabUnit.UnitConfig.Weapon.Damage;
        public UnitConfig UnitConfig => _prefabUnit.UnitConfig;
        public string Ability => _ability;
    }
}