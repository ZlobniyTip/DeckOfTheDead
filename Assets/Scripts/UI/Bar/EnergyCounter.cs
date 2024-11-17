using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnergyCounter : MonoBehaviour
    {
        [SerializeField] private Slider _barFilling;
        [SerializeField] private PlayerEnergy _playerEnergy;
        [SerializeField] private TMP_Text _energyCount;

        private Coroutine _changeValue;
        private float _recoveryRate = 0.1f;

        private void OnEnable()
        {
            _playerEnergy.EnergyChanged += StartTimer;
            _playerEnergy.EnergyChanged += ChangeEnergyCount;
        }

        private void OnDisable()
        {
            _playerEnergy.EnergyChanged -= StartTimer;
            _playerEnergy.EnergyChanged -= ChangeEnergyCount;
        }

        private void ChangeEnergyCount()
        {
            _energyCount.text = _playerEnergy.CurrentEnergyCount.ToString();
        }

        private void StartTimer()
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
                _barFilling.value = Mathf.MoveTowards(_barFilling.value, _barFilling.maxValue, _recoveryRate * Time.deltaTime);

                yield return null;
            }

            _barFilling.value = 0;
            _playerEnergy.IncreaseEnergy();

            yield break;
        }
    }
}