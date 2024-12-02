using UnityEngine;

[CreateAssetMenu(fileName = "New UnitConfig", menuName = "Config/Create new config", order = 51)]
public class UnitConfig : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _speed;

    public int Health => _health;
    public Weapon Weapon => _weapon;
    public float Speed => _speed;
}