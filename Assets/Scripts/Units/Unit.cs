using System;
using System.Collections;
using UnityEngine;

public class Unit : Health
{
    [SerializeField] private UnitConfig _config;

    private Character _character;
    private Enemy _target;
    private float _delayBetweenDeath = 2f;

    public UnitConfig UnitConfig => _config;
    public Enemy Target => _target;
    public Character Character => _character;

    private void Awake()
    {
        _maxValue = _config.Health;
        _value = _maxValue;
    }

    public void SetTarget(Enemy target)
    {
        _target = target;
    }

    public void SetCharacter(Character character)
    {
        _character = character;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (_value <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        var delay = new WaitForSeconds(_delayBetweenDeath);
        yield return delay;

        Destroy(gameObject);
    }
}