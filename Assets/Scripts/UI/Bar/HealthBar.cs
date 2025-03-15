using Other;
using UnityEngine;

namespace UI.Bar
{
    public class HealthBar : Bar
    {
        [SerializeField] private Health _health;

        private void Awake()
        {
            RecoveryRate = 0.8f;
            BarFilling.value = _health.MaxValueHealth;
        }

        private void OnEnable()
        {
            _health.HealthChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= OnValueChanged;
        }
    }
}