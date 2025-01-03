using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterShooting : MonoBehaviour, IAim
{
    [SerializeField] private CharacterScaning _characterScaning;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private ParticleSystem _weaponSpawn;
    [SerializeField] private TMP_Text _tectTime;

    private Enemy _currentEnemy;
    private Weapon _currentWeapon;
    private Weapon _previousWeapons;
    private int _score;
    private int _time;

    public bool IsShooting { get; private set; } = false;
    public Weapon CurrentWeapon => _currentWeapon;
    public int Score => _score;
    public Enemy Target => _currentEnemy;

    public event Action ChangedWeapon;

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

        if (_currentWeapon != null)
        {
            _previousWeapons = _currentWeapon;
            Destroy(_currentWeapon.gameObject);
        }

        _currentWeapon = Instantiate(weapon, _weaponPoint);
    }

    public void StartWeaponTimer(int time)
    {
        StartCoroutine(WeaponTimer(time));
    }

    private IEnumerator WeaponTimer(int time)
    {
        _time = time;
        _tectTime.gameObject.SetActive(true);

        while (_time > 0)
        {
            _tectTime.text = _time.ToString();
            yield return new WaitForSeconds(1);
            _time--;
        }

        Destroy(_currentWeapon.gameObject);
        _currentWeapon = Instantiate(_previousWeapons, _weaponPoint);
        _tectTime.gameObject.SetActive(false);
    }

    public void PlayWeaponSpawnEffect() => _weaponSpawn.Play();

    private IEnumerator Shooting()
    {
        var delay = new WaitForSeconds(_currentWeapon.DelayBetweenShots);

        while (_currentEnemy != null)
        {
            TurnToTarget();
            _currentEnemy.TakeDamage(_currentWeapon.Shoot());

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