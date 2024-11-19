using UnityEngine;

public class Unit : Health
{
    [SerializeField] private UnitConfig _config;

    private Character _character;
    private Enemy _target;

    public UnitConfig UnitConfig => _config;
    public Enemy Target => _target;
    public Character Character => _character;

    private void Awake()
    {
        _maxValue = _config.Health;
    }

    public void SetTarget(Enemy target)
    {
        _target = target;
    }

    public void SetCharacter(Character character)
    {
        _character = character;
    }
}
