using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class UnitMovement : MonoBehaviour
    {
        private Unit _unit;
        private UnitAttack _unitAttack;
        private NavMeshAgent _navMesh;
        private UnitAnimator _unitAnimator;
        private bool _cameUp;

        public NavMeshAgent NavMeshAgent => _navMesh;

        public bool CameUp => _cameUp;

        private void Awake()
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _unit = GetComponent<Unit>();
            _unitAttack = GetComponent<UnitAttack>();
            _unitAnimator = GetComponent<UnitAnimator>();
        }

        private void OnEnable()
        {
            StartCoroutine(SetNavMeshSpeed());
        }

        private void Update()
        {
            if (_unit.Target == null)
            {
                float distansToCharacterX = transform.position.x - _unit.Character.transform.position.x;

                if (distansToCharacterX < 4)
                {
                    MoveForward();
                }
                else
                {
                    StopMovement();

                    if (_unitAttack.IsAttacking == false)
                    {
                        _unitAnimator.PlauIdlingAnimation(_unitAttack.CurrentWeapon.WeaponKind);
                    }
                }
            }
            else
            {
                float distansToTarget = Vector3.Distance(transform.position, _unit.Target.transform.position);

                if (distansToTarget > _unitAttack.CurrentWeapon.AttackRange)
                {
                    _cameUp = false;
                    MoveToTarget();
                }
                else
                {
                    _cameUp = true;
                    StopMovement();
                }
            }
        }

        public void StopMovement()
        {
            _navMesh.speed = 0;
        }

        private void MoveToTarget()
        {
            _unitAnimator.PlauRunningAnimation(_unitAttack.CurrentWeapon.WeaponKind);
            _navMesh.speed = _unit.UnitConfig.Speed;
            _navMesh.SetDestination(_unit.Target.transform.position);
        }

        private void MoveForward()
        {
            _unitAnimator.PlauRunningAnimation(_unitAttack.CurrentWeapon.WeaponKind);
            _navMesh.speed = _unit.UnitConfig.Speed;
            Vector3 forwardPosition = transform.position + Vector3.right * 5.0f;
            _navMesh.SetDestination(forwardPosition);
        }

        private IEnumerator SetNavMeshSpeed()
        {
            yield return null;
            _navMesh.speed = _unit.UnitConfig.Speed;
        }
    }
}