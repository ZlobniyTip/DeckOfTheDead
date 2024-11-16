using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Create new card", order = 51)]
public class Card : ScriptableObject
{
    [SerializeField] private Sprite _icon;

    [SerializeField] private string _name;
    [SerializeField] private int _energy;
    [SerializeField] private int _level;

    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;

    [SerializeField] private string _ability;

    public Sprite Icon => _icon;
    public string Name => _name;
    public int Energy => _energy;
    public int Level => _level;
    public int Health => _health;
    public int Damage => _damage;
    public int Speed => _speed;
    public string Ability => _ability;

}
