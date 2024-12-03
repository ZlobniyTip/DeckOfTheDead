using System.Collections;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;

    private UnitSearchTarget _searchTarget;
    private UnitMovement _unitMovement;
    private Unit _unit;
    private float _distance;
    private Weapon _currentWeapon;
    private UnitAnimator _unitAnimator;

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

    public void ActivateAttack(Enemy enemy)
    {
        StopCoroutine(_searchTarget.SearchTarget());
        StartCoroutine(Attacking());
    }

    private void Start()
    {
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_currentWeapon.DelayBetweenShots);

        while (_unit.Target != null)
        {
            transform.LookAt(_unit.Target.transform);
            _distance = Vector3.Distance(transform.position, _unit.Target.transform.position);

            if (_distance <= _currentWeapon.AttackDistance)
            {
                _unitAnimator.PlauAttackAnimation(_currentWeapon.WeaponType);
                IsAttacking = true;

                _unit.Target.TakeDamage(_currentWeapon.Damage);

                yield return delay;
            }

            yield return null;
        }

        IsAttacking = false;
        StartCoroutine(_searchTarget.SearchTarget());
    }

    public void InstallWeapon()
    {
        _currentWeapon = _unit.UnitConfig.Weapon;
        _currentWeapon = Instantiate(_unit.UnitConfig.Weapon, _weaponPoint);
    }
}
