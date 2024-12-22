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

    private Unit _unit;
    private Molotov _currentMolotov;
    private Rigidbody _rbMolotov;
    private UnitAnimator _animator;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _animator = GetComponent<UnitAnimator>();
    }

    private void Start()
    {
        StartCoroutine(WaitingThrow());
    }

    private void OnDisable()
    {
        StopCoroutine(WaitingThrow());
    }

    private IEnumerator WaitingThrow()
    {
        var delayBetweenThrow = new WaitForSeconds(_cooldownThrow);

        while (true)
        {
            yield return delayBetweenThrow;

            if (_unit.Target != null)
            {
                Throw(_unit.Target.transform);
            }
        }
    }

    private void Throw(Transform target)
    {
        _molotovSource.Play();

        _currentMolotov = Instantiate(_molotov, _startingPoint);
        _rbMolotov = _currentMolotov.GetComponent<Rigidbody>();

        Vector3 delta = target.position - transform.position;

        _currentMolotov.transform.parent = null;
        _rbMolotov.velocity = delta * _velocityMult;
        _animator.PlayThrows();
    }
}