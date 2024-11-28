using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IProduct
{
    [SerializeField] protected WeaponType _weaponType;

    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private int _index;

    [SerializeField] protected float _attackDistance;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _delayBetweenShots;

    [NonSerialized] private ItemState _state = null;

    protected AudioSource _audio;
    protected bool _isShooting = false;

    public WeaponType WeaponType => _weaponType;
    public float DelayBetweenShots => _delayBetweenShots;
    public float AttackDistance => _attackDistance;

    public ItemType Type => _type;
    public Sprite Icon => _icon;
    public string Name => _name;
    public int Price => _price;
    public int Index => _index;
    public ItemState State => _state ??= new ItemState(ItemStatus.NotPurchased);

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public virtual int Shooting()
    {
        _audio.Play();
        return _damage;
    }

    public void StopShooting()
    {
        _isShooting = false;
    }

    public void Init(ItemStatus state)
    {
        State.SetStatus(state);
    }
}