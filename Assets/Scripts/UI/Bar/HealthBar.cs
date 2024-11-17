using UnityEngine;

namespace UI
{
    public class HealthBar : Bar
    {
        [SerializeField] private Health _health;

        private void Awake()
        {
            RecoveryRate = 0.8f;
            _barFilling.value = _health.MaxValue;
        }

        private void OnEnable()
        {
            _health.Changed += OnValueChanged;
        }

        private void OnDisable()
        {
            _health.Changed -= OnValueChanged;
        }
    }
}