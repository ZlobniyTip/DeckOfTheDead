using System.Collections;
using UnityEngine;
using Weapons;

namespace Units.Skills
{
    [RequireComponent(typeof(Unit))]
    public class CoolManThrower : Skill
    {
        [SerializeField] private Molotov _molotov;
        [SerializeField] private Transform _startingPoint;
        [SerializeField] private AnimationCurve _flightCurve;
        [SerializeField] private float _flightDuration = 0.4f;
        [SerializeField] private float _cooldownThrow;
        [SerializeField] private AudioSource _molotovSource;

        private Unit _unit;
        private UnitAnimator _animator;
        private UnitObserver _unitObserver;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _animator = GetComponent<UnitAnimator>();
            _unitObserver = GetComponent<UnitObserver>();
        }

        private void OnEnable() => StartCoroutine(ThrowRoutine());

        private void OnDisable() => StopCoroutine(ThrowRoutine());

        private IEnumerator ThrowRoutine()
        {
            var delay = new WaitForSeconds(_cooldownThrow);

            while (true)
            {
                yield return delay;

                if (_unit.Target != null)
                    yield return PerformThrow(_unit.Target.transform);
            }
        }

        private IEnumerator PerformThrow(Transform target)
        {
            _unitObserver.BlockAttack(true);
            _animator.PlayThrows();

            yield return new WaitForSeconds(0.5f);
            _molotovSource.Play();
            var molotovInstance = Instantiate(_molotov, _startingPoint.position, Quaternion.identity);

            yield return StartCoroutine(PerformArchedFlight(molotovInstance, target.position));
            _unitObserver.BlockAttack(false);
        }

        private IEnumerator PerformArchedFlight(Molotov molotovInstance, Vector3 targetPosition)
        {
            Vector3 startPosition = _startingPoint.position;
            float elapsedTime = 0f;

            while (elapsedTime < _flightDuration)
            {
                elapsedTime += Time.deltaTime;

                float progress = Mathf.Clamp01(elapsedTime / _flightDuration);
                Vector3 horizontalPosition = Vector3.Lerp(startPosition, targetPosition, progress);
                float verticalOffset = _flightCurve.Evaluate(progress);
                Vector3 newPosition = horizontalPosition + Vector3.up * verticalOffset;

                molotovInstance.transform.position = newPosition;

                yield return null;
            }

            molotovInstance.transform.position = targetPosition;
        }
    }
}