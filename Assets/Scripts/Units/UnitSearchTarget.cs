using System.Collections;
using Enemy;
using UnityEngine;

namespace Units
{
    public class UnitSearchTarget : MonoBehaviour
    {
        private readonly Collider[] OverlappedColliders = new Collider[10];

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
            if (_unit.Attack.CurrentWeapon != null && _unit.Attack.CurrentWeapon.AttackRange > _radius)
            {
                _radius = _unitAttack.CurrentWeapon.AttackRange;
            }

            while (true)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, OverlappedColliders);
                Rigidbody rigidbody;

                for (int i = 0; i < count; i++)
                {
                    rigidbody = OverlappedColliders[i].attachedRigidbody;

                    if (rigidbody)
                    {
                        if (rigidbody.gameObject.TryGetComponent(out Zombie enemy) && enemy.IsDiying == false)
                        {
                            _unit.SetTarget(enemy);
                        }
                    }
                }

                yield return 0.1f;
            }
        }
    }
}