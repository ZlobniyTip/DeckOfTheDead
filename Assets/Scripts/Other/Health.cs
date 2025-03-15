using System;
using Enemy;
using UnityEngine;

namespace Other
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] private int _maxValue;

        protected int MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public event Action<int, int> HealthChanged;
        public event Action Died;

        protected int Value { get; set; }
        protected Zombie LastAttacker { get; set; }

        public bool IsDiying { get; private set; } = false;
        public int MaxValueHealth => MaxValue;
        public int ValueHealth => Value;

        public virtual void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                Value -= damage;
                HealthChanged?.Invoke(Value, MaxValue);
            }
        }

        public virtual void TakeHeal(int healValue)
        {
            if (Value + healValue <= MaxValue)
            {
                Value += healValue;
                HealthChanged?.Invoke(Value, MaxValue);
            }
        }

        public void TakeDamageFromEnemy(int damage, Zombie attacker)
        {
            LastAttacker = attacker;
            TakeDamage(damage);
        }

        public void SetValue(int value, int maxValue)
        {
            Value = value;
            MaxValue = maxValue;
            HealthChanged?.Invoke(Value, MaxValue);
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