using UnityEngine;

public class UnitController : MonoBehaviour
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

        DisableStates();
    }

    private void Start()
    {
        _unitMovement.enabled = true;
    }

    private void Update()
    {
        if (_unit.Target == null)
        {
            _unitSearchTarget.enabled = true;
            _unitAttack.enabled = false;
        }
        else
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
    }
}
