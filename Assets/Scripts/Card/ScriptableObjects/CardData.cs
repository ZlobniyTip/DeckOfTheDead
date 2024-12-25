using System;
using UnityEngine;

public abstract class CardData : ScriptableObject, IProduct
{
    [SerializeField] private Sprite _icon;

    [SerializeField] private string _name;
    [SerializeField] private int _energy;
    [SerializeField] private int _level;

    [SerializeField] private ItemType _type;
    [SerializeField] private int _price;
    [SerializeField] private int _index;

    [NonSerialized] private ItemState _state = null;

    public Sprite Icon => _icon;
    public string Name => _name;
    public int Energy => _energy;
    public int Level => _level;
    public ItemType Type => _type;
    public int Price => _price;
    public int Index => _index;
    public ItemState State => _state ??= new ItemState(ItemStatus.NotPurchased);

    public void Init(ItemStatus state)
    {
        State.SetStatus(state);
    }
}