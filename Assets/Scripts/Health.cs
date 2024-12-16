using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int _maxValue;

    protected int _value;

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
    public void AddHealth(int count)
    {
        if (_value < _maxValue)
        {
            _value += count;

            if(_value > _maxValue)
            {
                _value = _maxValue;
            }

            Changed?.Invoke(_value, _maxValue);
        }
    }
}