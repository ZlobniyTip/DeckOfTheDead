using Enemy;
using System;
using UnityEngine;

namespace Other
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] protected int MaxValue;

        protected int Value;
        protected Zombie LastAttacker;

        public event Action<int, int> Changed;
        public event Action Died;

        public bool IsDiying { get; private set; } = false;
        public int MaxValueHealth => MaxValue;
        public int ValueHealth => Value;

        public virtual void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                Value -= damage;
                Changed?.Invoke(Value, MaxValue);
            }
        }

        public virtual void TakeHeal(int healValue)
        {
            if (Value + healValue <= MaxValue)
            {
                Value += healValue;
                Changed?.Invoke(Value, MaxValue);
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
            Changed?.Invoke(Value, MaxValue);
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