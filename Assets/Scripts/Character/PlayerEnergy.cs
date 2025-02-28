using System;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class PlayerEnergy : MonoBehaviour
    {
        private readonly int MaxEnergyCount = 10;

        [SerializeField] private ParticleSystem _energyProduction;
        [SerializeField] private AudioSource _audioSource;

        private int _currentEnergyCount = 0;

        public int CurrentEnergyCount => _currentEnergyCount;

        public event Action EnergyChanged;

        private void Start()
        {
            IncreaseEnergy();
            EnergyChanged?.Invoke();
        }

        public void IncreaseEnergy()
        {
            if (_currentEnergyCount < MaxEnergyCount)
            {
                _currentEnergyCount++;
                EnergyChanged?.Invoke();
            }
        }

        public IEnumerator IncreaseEnergyJournalist()
        {
            yield return new WaitForSeconds(0.5f);

            if (_audioSource != null)
                _audioSource.Play();

            _energyProduction.Play();
            IncreaseEnergy();
        }

        public void UseUpEnergy(int energy)
        {
            if (_currentEnergyCount >= energy)
            {
                _currentEnergyCount -= energy;
                EnergyChanged?.Invoke();
            }
        }
    }
}