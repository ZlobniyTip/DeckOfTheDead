using System.Collections;
using Enemy.Skills;
using UnityEngine;

namespace Units.Skills
{
    public class PoisonRain : Skill
    {
        [SerializeField] private Rain _rain;
        [SerializeField] private float _cooldown;

        private Rain _currentRain;

        private void Start()
        {
            StartCoroutine(CallPoisonRain());
        }

        public override void OnUseSkill()
        {
            _currentRain = Instantiate(_rain, transform.position, Quaternion.identity);
            _currentRain.transform.parent = null;
        }

        private IEnumerator CallPoisonRain()
        {
            var delay = new WaitForSeconds(_cooldown);

            while (enabled)
            {
                OnUseSkill();

                yield return delay;
            }
        }
    }
}