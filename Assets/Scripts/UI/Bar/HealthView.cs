namespace UI
{
    public class HealthView : Bar
    {
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