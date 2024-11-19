using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSearchTarget : MonoBehaviour
{
    [SerializeField] private float _radius;

    private UnitAttack _unitAttack;
    private Unit _unit;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _unitAttack = GetComponent<UnitAttack>();
    }

    private void Start()
    {
        StartCoroutine(SearchTarget());
    }

    public IEnumerator SearchTarget()
    {
        while (_unit.Target == null)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;

                if (rigidbody)
                {
                    if (rigidbody.gameObject.TryGetComponent(out Enemy enemy))
                    {
                        _unit.SetTarget(enemy);
                        _unitAttack.ActivateAttack(enemy);
                    }
                }
            }

            yield return null;
        }
    }
}
