using System.Collections;
using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public class EnergyCounter : MonoBehaviour
    {
        private readonly float RecoveryRate = 0.18f;

        [SerializeField] private Slider _barFilling;
        [SerializeField] private PlayerEnergy _playerEnergy;
        [SerializeField] private TMP_Text _energyCount;

        private Coroutine _changeValue;

        private void OnEnable()
        {
            _playerEnergy.EnergyChanged += OnStartTimer;
            _playerEnergy.EnergyChanged += OnChangeEnergyCount;
        }

        private void OnDisable()
        {
            _playerEnergy.EnergyChanged -= OnStartTimer;
            _playerEnergy.EnergyChanged -= OnChangeEnergyCount;
        }

        private void OnChangeEnergyCount()
        {
            _energyCount.text = _playerEnergy.CurrentEnergyCount.ToString();
        }

        private void OnStartTimer()
        {
            if (_changeValue != null)
            {
                StopCoroutine(_changeValue);
            }

            _changeValue = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (_barFilling.value != _barFilling.maxValue)
            {
                _barFilling.value = 
                    Mathf.MoveTowards(_barFilling.value, _barFilling.maxValue, RecoveryRate * Time.deltaTime);

                yield return null;
            }

            _barFilling.value = 0;
            _playerEnergy.IncreaseEnergy();

            yield break;
        }
    }
}