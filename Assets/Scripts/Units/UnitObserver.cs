using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(UnitAttack))]
    [RequireComponent(typeof(UnitSearchTarget))]
    [RequireComponent(typeof(UnitMovement))]
    [RequireComponent(typeof(Unit))]
    public class UnitObserver : MonoBehaviour
    {
        private UnitSearchTarget _unitSearchTarget;
        private UnitAttack _unitAttack;
        private UnitMovement _unitMovement;
        private Unit _unit;

        private Coroutine _currentRoutine;
        private bool _isAttackBlocked = false;

        private void Awake()
        {
            _unitSearchTarget = GetComponent<UnitSearchTarget>();
            _unitAttack = GetComponent<UnitAttack>();
            _unitMovement = GetComponent<UnitMovement>();
            _unit = GetComponent<Unit>();
        }

        private void Start()
        {
            DisableStates();

            _unitMovement.enabled = true;
            enabled = true;
        }

        private void Update()
        {
            if (_isAttackBlocked)
            {
                _unitAttack.enabled = false;
                return;
            }

            if (_unit.Target == null)
            {
                _unitSearchTarget.enabled = true;
                _unitAttack.enabled = false;
            }
            else if (_unit.Target != null && _unitMovement.CameUp == true)
            {
                _unitSearchTarget.enabled = false;
                _unitAttack.enabled = true;
            }
        }

        public void DisableStates()
        {
            _unitSearchTarget.enabled = false;
            _unitAttack.enabled = false;
            _unitMovement.enabled = false;
            enabled = false;
        }

        public void BlockAttack(bool block)
        {
            _isAttackBlocked = block;

            if (block)
                _unitAttack.enabled = false;
        }
    }
}