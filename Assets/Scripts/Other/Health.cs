using Enemy;
using System;
using UnityEngine;

namespace Other
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] protected int _maxValue;

        protected int _value;
        protected Zombie _lastAttacker;

        public event Action<int, int> Changed;
        public event Action Died;

        public bool IsDiying { get; private set; } = false;
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

        public virtual void TakeHeal(int healValue)
        {
            if (_value + healValue <= _maxValue)
            {
                _value += healValue;
                Changed?.Invoke(_value, _maxValue);
            }
        }

        public void TakeDamageFromEnemy(int damage, Zombie attacker)
        {
            _lastAttacker = attacker;
            TakeDamage(damage);
        }

        public void SetValue(int value, int maxValue)
        {
            _value = value;
            _maxValue = maxValue;
            Changed?.Invoke(_value, _maxValue);
        }

        public void SetDiyingStatus(bool isDiying)
        {
            IsDiying = isDiying;
        }

        public void DeclareDeath()
        {
            Died?.Invoke();
        }
    }
}