using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterShooting : MonoBehaviour, IAim
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterScaning _characterScaning;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private ParticleSystem _weaponSpawn;
    [SerializeField] private TMP_Text _tectTime;

    private Zombie _currentEnemy;
    private Weapon _currentWeapon;
    private Weapon _previousWeapons;
    private Weapon _removableWeapons;
    private int _time;

    public bool IsShooting { get; private set; } = false;
    public Weapon CurrentWeapon => _currentWeapon;
    public Zombie Target => _currentEnemy;

    public event Action ChangedWeapon;
    public event Action KilledTarget;
    public event Action<int> CausedDamage;

    private void Awake()
    {
        EquipWeapon(_defaultWeapon, null);
    }

    public void ActivShooting(Zombie enemy)
    {
        _currentEnemy = enemy;
        IsShooting = true;
        _characterScaning.StopSearch();
        StartCoroutine(Shooting());
    }

    public void EquipWeapon(Weapon weapon, Action equipmentChanged)
    {
        weapon.State.SetStatus(ItemStatus.Equipped);

        equipmentChanged?.Invoke();
        ChangedWeapon?.Invoke();

        if (_currentWeapon != null)
        {
            _removableWeapons = _currentWeapon;
            _currentWeapon = Instantiate(weapon, _weaponPoint);
            Destroy(_removableWeapons.gameObject);
        }
        else
        {
            _currentWeapon = Instantiate(weapon, _weaponPoint);
        }
    }

    public void UseTemporaryWeapons(Weapon weapon, CardView view)
    {
        ChangedWeapon?.Invoke();
        _previousWeapons = _currentWeapon;
        _currentWeapon = Instantiate(weapon, _weaponPoint);
        _currentWeapon.ApplyGain(view);
        _previousWeapons.gameObject.SetActive(false);
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

        _removableWeapons = _currentWeapon;
        _previousWeapons.gameObject.SetActive(true);
        _currentWeapon = Instantiate(_previousWeapons, _weaponPoint);

        Destroy(_removableWeapons.gameObject);
        Destroy(_previousWeapons.gameObject);

        _tectTime.gameObject.SetActive(false);
    }

    public void PlayWeaponSpawnEffect() => _weaponSpawn.Play();

    public void StopShooting()
    {
        StopCoroutine(Shooting());

        if (_currentWeapon.WeaponType == WeaponType.FlameThrower)
        {
            FlameThrower flame = _currentWeapon as FlameThrower;
            flame.StopEffect();
        }
    }

    private IEnumerator Shooting()
    {
        var delay = new WaitForSeconds(_currentWeapon.DelayBetweenShots);

        while (_currentEnemy.IsDiying == false)
        {
            TurnToTarget();
            _currentEnemy.TakeDamage(_currentWeapon.Shoot());

            CausedDamage?.Invoke(_currentWeapon.Shoot());

            yield return delay;
        }

        if (_currentWeapon.WeaponType == WeaponType.FlameThrower)
        {
            _currentWeapon.StopShooting();
        }

        KilledTarget?.Invoke();
        IsShooting = false;
        _character.GetLeaderboardScore(_currentEnemy.Reward);
        _characterScaning.ActivSearch();
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