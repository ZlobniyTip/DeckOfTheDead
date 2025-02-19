using Spawner;
using UnityEngine;

namespace UI.Bar
{
    public class ProgressBar : Bar
    {
        [SerializeField] private ZombieSpawner _spawner;

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