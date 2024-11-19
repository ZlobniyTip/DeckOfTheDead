using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Create new card", order = 51)]
public class Card : ScriptableObject
{
    [SerializeField] private UnitConfig _unitConfig;
    [SerializeField] private Sprite _icon;

    [SerializeField] private string _name;
    [SerializeField] private int _energy;
    [SerializeField] private int _level;

    [SerializeField] private string _ability;

    public Sprite Icon => _icon;
    public string Name => _name;
    public int Energy => _energy;
    public int Level => _level;
    public int Health => _unitConfig.Health;
    public int Damage => _unitConfig.Damage;
    public float Speed => _unitConfig.Speed;
    public string Ability => _ability;
}