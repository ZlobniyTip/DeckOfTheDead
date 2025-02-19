using Other;
using UnityEngine;

namespace Units.Skills
{
    public class PoliceAmmunition : Health
    {
        [SerializeField] private string _name;

        public string Name => _name;

        private void Awake()
        {
            _value = _maxValue;
        }
    }
}