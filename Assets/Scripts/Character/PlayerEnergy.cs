using System;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private int _maxEnergyCount = 10;
    private int _currentEnergyCount = 0;

    public event Action EnergyChanged;

    public int CurrentEnergyCount => _currentEnergyCount;

    private void Start()
    {
        IncreaseEnergy();
    }

    public void IncreaseEnergy()
    {
        EnergyChanged?.Invoke();

        if (_currentEnergyCount < _maxEnergyCount)
        {
            _currentEnergyCount++;
        }
    }
}