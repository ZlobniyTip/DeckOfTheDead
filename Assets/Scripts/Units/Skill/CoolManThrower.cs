using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class CoolManThrower : Skill
{
    [SerializeField] private Molotov _molotov;
    [SerializeField] private Transform _startingPoint;
    [SerializeField] private float _velocityMult;
    [SerializeField] private float _cooldownThrow;
    [SerializeField] private AudioSource _molotovSource;
    [SerializeField] private float _throwAnimationDuration = 2.0f;

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
    private void OnDisable() => StopAllCoroutines();

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
        _unitObserver?.BlockAttack(true);
        _animator.PlayThrows();

        yield return new WaitForSeconds(0.5f);

        _molotovSource.Play();
        var molotovInstance = Instantiate(_molotov, _startingPoint.position, Quaternion.identity);
        var rbMolotov = molotovInstance.GetComponent<Rigidbody>();
        rbMolotov.velocity = (target.position - transform.position) * _velocityMult;

        yield return new WaitForSeconds(_throwAnimationDuration);
        _unitObserver?.BlockAttack(false);
    }
}