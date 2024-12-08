using System.Collections;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;

    private UnitSearchTarget _searchTarget;
    private UnitMovement _unitMovement;
    private Unit _unit;
    private Weapon _currentWeapon;
    private UnitAnimator _unitAnimator;
    private Coroutine _coroutine;

    public bool IsAttacking { get; private set; } = false;
    public Weapon CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _unitMovement = GetComponent<UnitMovement>();
        _searchTarget = GetComponent<UnitSearchTarget>();
        _unitAnimator = GetComponent<UnitAnimator>();
        InstallWeapon();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Attacking());
        IsAttacking = true;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        IsAttacking = false;
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_currentWeapon.DelayBetweenShots);

        while (true)
        {
            transform.LookAt(_unit.Target.transform);
            var distance = Vector3.Distance(transform.position, _unit.Target.transform.position);

            if (distance <= _currentWeapon.AttackDistance)
            {    
                _unitAnimator.PlauAttackAnimation(_currentWeapon.WeaponType);

                _unit.Target.TakeDamage(_currentWeapon.Shoot());

                yield return delay;
            }

            yield return null;
        }
    }

    public void InstallWeapon()
    {
        _currentWeapon = _unit.UnitConfig.Weapon;
        _currentWeapon = Instantiate(_unit.UnitConfig.Weapon, _weaponPoint);
    }
}
