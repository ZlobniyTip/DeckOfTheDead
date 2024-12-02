using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Create new card", order = 51)]
public class Card : ScriptableObject, IProduct
{
    [SerializeField] private Unit _prefabUnit;
    [SerializeField] private UnitConfig _unitConfig;
    [SerializeField] private Sprite _icon;

    [SerializeField] private string _name;
    [SerializeField] private int _energy;
    [SerializeField] private int _level;

    [SerializeField] private ItemType _type;
    [SerializeField] private int _price;
    [SerializeField] private int _index;

    [SerializeField] private string _ability;

    [NonSerialized] private ItemState _state = null;

    public Unit PrefabUnit => _prefabUnit;
    public Sprite Icon => _icon;
    public string Name => _name;
    public int Energy => _energy;
    public int Level => _level;
    public int Health => _unitConfig.Health;
    public int Damage => _unitConfig.Weapon.Damage;
    public float Speed => _unitConfig.Speed;
    public string Ability => _ability;

    public ItemType Type => _type;
    public int Price => _price;
    public int Index => _index;
    public ItemState State => _state ??= new ItemState(ItemStatus.NotPurchased);

    public void Init(ItemStatus state)
    {
        State.SetStatus(state);
    }
}