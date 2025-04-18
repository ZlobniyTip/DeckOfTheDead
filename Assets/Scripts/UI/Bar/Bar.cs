using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] private Slider _barFilling;

        private Coroutine _changeValue;

        public Slider BarFilling => _barFilling;

        protected float RecoveryRate { get; set; } = 0.2f;

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
            while (BarFilling.value != target)
            {
                BarFilling.value = 
                    Mathf.MoveTowards(BarFilling.value, target, RecoveryRate * Time.deltaTime);

                yield return null;
            }

            yield break;
        }
    }
}