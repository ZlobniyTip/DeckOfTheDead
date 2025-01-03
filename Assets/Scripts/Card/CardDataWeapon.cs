using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Create new weapon", order = 51)]
public class CardDataWeapon : CardData
{
    [SerializeField] private Weapon _prefabWeapon;
    [SerializeField] private int _timeAction;

    public Weapon PrefabWeapon => _prefabWeapon;
    public float WeaponDamage => _prefabWeapon.Damage;
    public float DelayBetweenShots => _prefabWeapon.DelayBetweenShots;
    public int TimeAction => _timeAction;
}
