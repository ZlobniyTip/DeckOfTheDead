using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour, IProduct
{
    [SerializeField] protected WeaponStatus _weaponStatus;

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

    public WeaponStatus WeaponStatus => _weaponStatus;
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

    public void Init(ItemStatus state)
    {
        State.SetStatus(state);
    }
}