using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponStatus _weaponStatus;

    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _name;
    [SerializeField] protected int _price;

    [SerializeField] protected float _attackDistance;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _delayBetweenShots;

    public float DelayBetweenShots => _delayBetweenShots;
    public WeaponStatus WeaponStatus => _weaponStatus;
    public float AttackDistance => _attackDistance;

    public Sprite Icon => _icon;
    public string Name => _name;
    public int Price => _price;

    public virtual int Shooting()
    {
        return _damage;
    }
}
