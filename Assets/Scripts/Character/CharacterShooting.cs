using System;
using System.Collections;
using UnityEngine;

public class CharacterShooting : MonoBehaviour, IAim
{
    [SerializeField] private CharacterScaning _characterScaning;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Weapon _defaultWeapon;

    private Enemy _currentEnemy;
    private Weapon _currentWeapon;
    private int _score;

    public event Action ChangedWeapon;

    public bool IsShooting { get; private set; } = false;

    public Weapon CurrentWeapon => _currentWeapon;
    public int Score => _score;

    public Enemy Target => _currentEnemy;

    private void Awake()
    {
        EquipWeapon(_defaultWeapon, null);
    }

    public void LoadScore(int score)
    {
        _score = score;
    }

    public void ActivShooting(Enemy enemy)
    {
        _currentEnemy = enemy;
        IsShooting = true;
        StopCoroutine(_characterScaning.SearchEnemy());
        StartCoroutine(Shooting());
    }

    public void EquipWeapon(Weapon weapon, Action equipmentChanged)
    {
        weapon.State.SetStatus(ItemStatus.Equipped);

        equipmentChanged?.Invoke();
        ChangedWeapon?.Invoke();

        Destroy(_currentWeapon);
        _currentWeapon = Instantiate(weapon, _weaponPoint);
    }

    private IEnumerator Shooting()
    {
        var delay = new WaitForSeconds(_currentWeapon.DelayBetweenShots);

        while (_currentEnemy != null)
        {
            TurnToTarget();
            //_currentEnemy.TakeDamage(_currentWeapon.Shoot());

            yield return delay;
        }

        if (_currentWeapon.WeaponType == WeaponType.FlameThrower)
        {
            _currentWeapon.StopShooting();
        }

        IsShooting = false;
        StartCoroutine(_characterScaning.SearchEnemy());
    }

    private void TurnToTarget()
    {
        if (_currentEnemy != null)
        {
            Vector3 direction = _currentEnemy.transform.position - transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
}