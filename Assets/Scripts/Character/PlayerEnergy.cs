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

    public void TakeEnergy(int energy)
    {
        if(_currentEnergyCount >= energy)
        {
            _currentEnergyCount -= energy;
            EnergyChanged?.Invoke();
        } 
    }
}