using System.Collections;
using UnityEngine;

public class UnitSearchTarget : MonoBehaviour
{
    private float _radius = 4;
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
        if (_unitAttack.CurrentWeapon.AttackDistance > _radius)
        {
            _radius = _unitAttack.CurrentWeapon.AttackDistance;
        }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
