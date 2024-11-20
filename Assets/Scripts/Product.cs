using System;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private int _index;

    [NonSerialized] private ItemState _state = null;

    public ItemType Type => _type;
    public Sprite Icon => _icon;
    public string Name => _name;
    public int Price => _price;
    public int Index => _index;
    public ItemState State => _state ??= new ItemState(ItemStatus.NotPurchased);

    public void Init(ItemStatus state)
    {
        State.SetStatus(state);
    }
}