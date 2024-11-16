using System.Collections;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    [SerializeField] private CharacterScaning _characterScaning;
    [SerializeField] private Weapon _weaponPrefab;
    [SerializeField] private Transform _weaponPoint;

    private Enemy _currentEnemy;
    private Weapon _weapon;

    public bool IsShooting { get; private set; } = false;
    public Weapon CurrentWeapon => _weaponPrefab;

    private void Start()
    {
        _weapon = Instantiate(_weaponPrefab, _weaponPoint);
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

    private IEnumerator Shooting()
    {
        var delay = new WaitForSeconds(_weapon.DelayBetweenShots);

        while (_currentEnemy != null)
        {
            _currentEnemy.TakeDamage(_weapon.Shooting());

            yield return delay;
        }

        _currentEnemy = null;
        IsShooting = false;
        StartCoroutine(_characterScaning.SearchEnemy());
    }
}