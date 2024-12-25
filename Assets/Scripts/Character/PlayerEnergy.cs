using System;
using System.Collections;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _energyProduction;
    [SerializeField] private AudioSource _audioSource;

    private int _maxEnergyCount = 10;
    private int _currentEnergyCount = 0;

    public event Action EnergyChanged;

    public int CurrentEnergyCount => _currentEnergyCount;

    private void Start()
    {
        IncreaseEnergy();
        EnergyChanged?.Invoke();
    }

    public void IncreaseEnergy()
    {
        if (_currentEnergyCount < _maxEnergyCount)
        {
            _currentEnergyCount++;
            EnergyChanged?.Invoke();
        }
    }

    public IEnumerator IncreaseEnergyJournalist()
    {
        yield return new WaitForSeconds(0.5f);
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