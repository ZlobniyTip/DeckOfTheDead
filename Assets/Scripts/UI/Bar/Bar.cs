using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] protected Slider _barFilling;

        protected float RecoveryRate = 0.2f;

        private Coroutine _changeValue;

        public void OnValueChanged(int value, int maxValue)
        {
            if (_changeValue != null)
            {
                StopCoroutine(_changeValue);
            }

            _changeValue = StartCoroutine(ChangeHealthBar((float)value / maxValue));
        }

        private IEnumerator ChangeHealthBar(float target)
        {
            while (_barFilling.value != target)
            {
                _barFilling.value = Mathf.MoveTowards(_barFilling.value, target, RecoveryRate * Time.deltaTime);

                yield return null;
            }

            yield break;
        }
    }
}