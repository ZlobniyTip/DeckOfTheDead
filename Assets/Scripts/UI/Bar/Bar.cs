using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] protected Slider BarFilling;

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
            while (BarFilling.value != target)
            {
                BarFilling.value = Mathf.MoveTowards(BarFilling.value, target, RecoveryRate * Time.deltaTime);

                yield return null;
            }

            yield break;
        }
    }
}