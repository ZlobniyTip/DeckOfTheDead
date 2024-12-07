using UnityEngine;

public class UnitObserver : MonoBehaviour
{
    private UnitSearchTarget _unitSearchTarget;
    private UnitAttack _unitAttack;
    private UnitMovement _unitMovement;
    private Unit _unit;

    private Coroutine _currentRoutine;

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
        if (_unit.Target == null)
        {
            _unitSearchTarget.enabled = true;
            _unitAttack.enabled = false;
        }
        else if(_unit.Target != null && _unitMovement.CameUp == true)
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
}