using System.Collections;
using Units;
using UnityEngine;

namespace Enemy.Skills.ClawedStrike
{
    public class Bleeding : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private float _duration;
        [SerializeField] private float _cooldown;
        [SerializeField] private int _damage;

        private Unit _unit;
        private ParticleSystem _particleSystem;
        private float _timer = 0;

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        public void GetLinkUnit(Unit unit)
        {
            _unit = unit;
            _particleSystem = Instantiate(_effect, _unit.transform);
            StartCoroutine(StartBleeding());
        }

        private IEnumerator StartBleeding()
        {
            var delay = new WaitForSeconds(_cooldown);

            while (_timer < _duration)
            {
                _unit.TakeDamage(_damage);
                yield return delay;
            }

            Destroy(_particleSystem);
            Destroy(gameObject);
        }
    }
}