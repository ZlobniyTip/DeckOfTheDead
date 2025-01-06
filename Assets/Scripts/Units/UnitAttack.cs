using System.Collections;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;

    private Unit _unit;
    private Weapon _currentWeapon;
    private UnitAnimator _unitAnimator;
    private Coroutine _coroutine;
    private int _damage = 0;

    public float _distance;

    public bool IsAttacking { get; private set; } = false;
    public Weapon CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _unitAnimator = GetComponent<UnitAnimator>();
        InstallWeapon();
    }

    private void OnEnable()
    {
        IsAttacking = true;
        _coroutine = StartCoroutine(Attacking());
    }

    private void OnDisable()
    {
        IsAttacking = false;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void SetAdditionalDamage(int damage)
    {
        _damage = damage;
    }

    private IEnumerator Attacking()
    {
        if (_currentWeapon == null)
            yield break;

        var delay = new WaitForSeconds(_currentWeapon.DelayBetweenShots);

        while (IsAttacking)
        {
            TurnToTarget();

            if (_unit.Target != null)
            {
                _distance = Vector3.Distance(transform.position, _unit.Target.transform.position);

                if (_distance <= _currentWeapon.AttackDistance)
                {
                    _unitAnimator.PlauAttackAnimation(_currentWeapon.WeaponType);

                    if (_unit.Target != null)
                    {
                        _unit.Target.TakeDamage(_currentWeapon.Shoot() + _damage);
                    }

                    yield return delay;
                }
            }

            yield return 0.1f;
        }
    }

    private void TurnToTarget()
    {
        if (_unit.Target != null)
        {
            Vector3 direction = _unit.Target.transform.position - transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }

    private void InstallWeapon()
    {
        _currentWeapon = _unit.UnitConfig.Weapon;
        _currentWeapon = Instantiate(_unit.UnitConfig.Weapon, _weaponPoint);
    }
}
