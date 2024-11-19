using UnityEngine;

[CreateAssetMenu(fileName = "New UnitConfig", menuName = "Config/Create new config", order = 51)]
public class UnitConfig : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    public int Health => _health;
    public int Damage => _damage;
    public float Speed => _speed;
}