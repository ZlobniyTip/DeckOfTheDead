using System.Collections;
using UnityEngine;

public class UnitSearchTarget : MonoBehaviour
{
    private float _radius = 4;
    private UnitAttack _unitAttack;
    private Unit _unit;
    private Coroutine _coroutine;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _unitAttack = GetComponent<UnitAttack>();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(SearchTarget());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public IEnumerator SearchTarget()
    {
        if (_unit.Attack.CurrentWeapon != null && _unit.Attack.CurrentWeapon.AttackDistance > _radius)
        {
            _radius = _unitAttack.CurrentWeapon.AttackDistance;
        }

        while (true)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;

                if (rigidbody)
                {
                    if (rigidbody.gameObject.TryGetComponent(out Enemy enemy) && enemy.IsDiying == false)
                    {
                        _unit.SetTarget(enemy);
                    }
                }
            }

            yield return 0.1f;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, _radius);
    //}
}
