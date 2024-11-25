using System;
using System.Collections;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    [SerializeField] private CharacterScaning _characterScaning;
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private Transform _weaponPoint;

    private Enemy _currentEnemy;
    private Weapon _currentWeapon;

    public event Action ChangedWeapon;

    public bool IsShooting { get; private set; } = false;
    public Weapon CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        EquipWeapon(_defaultWeapon);
    }

    private void Update()
    {
        if (_currentEnemy != null)
        {
            transform.LookAt(_currentEnemy.transform);
        }
    }

    public void ActivShooting(Enemy enemy)
    {
        _currentEnemy = enemy;
        IsShooting = true;
        StopCoroutine(_characterScaning.SearchEnemy());
        StartCoroutine(Shooting());
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.State.SetStatus(ItemStatus.Purchased);
            Destroy(_currentWeapon);
        }

        _currentWeapon = Instantiate(weapon, _weaponPoint);
        _currentWeapon.State.SetStatus(ItemStatus.Equipped);

        ChangedWeapon?.Invoke();
    }

    private IEnumerator Shooting()
    {
        var delay = new WaitForSeconds(_currentWeapon.DelayBetweenShots);

        while (_currentEnemy != null)
        {
            _currentEnemy.TakeDamage(_currentWeapon.Shooting());

            yield return delay;
        }

        _currentEnemy = null;
        IsShooting = false;
        StartCoroutine(_characterScaning.SearchEnemy());
    }
}