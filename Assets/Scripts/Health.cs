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
        Changed?.Invoke(_value, _maxValue);

        if (damage > 0)
        {
            _value -= damage;  
        }
    }
}