using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class CoolManThrower : Skill
{
    [SerializeField] private Molotov _molotov;
    [SerializeField] private Transform _startingPoint;
    [SerializeField] private float _velocityMult;
    [SerializeField] private float _cooldownThrow;

    private Unit _unit;
    private Molotov _currentMolotov;
    private Rigidbody _rbMolotov;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    private void Start()
    {
        StartCoroutine(WaitingThrow());
    }

    private IEnumerator WaitingThrow()
    {
        var delayBetweenThrow = new WaitForSeconds(_cooldownThrow);

        while (true)
        {
            if (_unit.Target != null)
            {
                Throw(_unit.Target.transform);
            }

            yield return delayBetweenThrow;
        }
    }

    private void Throw(Transform target)
    {
        _currentMolotov = Instantiate(_molotov, _startingPoint);
        _rbMolotov = _currentMolotov.GetComponent<Rigidbody>();

        Vector3 delta = target.position - transform.position;

        _currentMolotov.transform.parent = null;
        _rbMolotov.velocity = delta * _velocityMult;
    }
}