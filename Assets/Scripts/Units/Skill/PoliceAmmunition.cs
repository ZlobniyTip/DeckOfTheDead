using UnityEngine;

public class PoliceAmmunition : Health
{
    [SerializeField] private string _name;

    public string Name => _name;

    private void Awake()
    {
        _value = _maxValue;
    }
}