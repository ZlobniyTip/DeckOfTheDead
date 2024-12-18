using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int _maxValue;

    protected int _value;
    protected Enemy _lastAttacker;

    public event Action<int, int> Changed;

    public int MaxValue => _maxValue;
    public int Value => _value;

    public virtual void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _value -= damage;
            Changed?.Invoke(_value, _maxValue);
        }
    }

    public void TakeDamageFromEnemy(int damage, Enemy attacker)
    {
        _lastAttacker = attacker; 
        TakeDamage(damage);       
    }

    public void AddHealth(int count)
    {
        if (_value < _maxValue)
        {
            _value += count;

            if(_value > _maxValue)
                _value = _maxValue;

            Changed?.Invoke(_value, _maxValue);
        }
    }

    public void SetValue(int value, int maxValue )
    {
        _value = value;
        _maxValue = maxValue;
        Changed?.Invoke(_value, _maxValue);
    }
}