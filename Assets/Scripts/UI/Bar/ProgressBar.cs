using UnityEngine;

namespace UI
{
    public class ProgressBar : Bar
    {
        [SerializeField] private Spawner _spawner;

        private void OnEnable()
        {
            _spawner.ReachedPoint += OnValueChanged;
        }

        private void OnDisable()
        {
            _spawner.ReachedPoint -= OnValueChanged;
        }
    }
}