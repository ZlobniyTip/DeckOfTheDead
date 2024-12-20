using System;
using System.Collections;
using UnityEngine;

public class Unit : Health, IAim
{
    [SerializeField] private UnitConfig _config;
    [SerializeField] private AudioSource _soundSpawn;
    [SerializeField] private PoliceAmmunition _policeArmour;

    private Character _character;
    private Enemy _target;
    private UnitMovement _movement;
    private UnitAttack _attack;
    private UnitSearchTarget _searchTarget;
    private UnitAnimator _unitAnimator;
    private UnitObserver _controller;
    private FXUnit _fxUnit;
    private float _delayBetweenDeath = 2.7f;

    public event Action Died;

    public UnitMovement Movement => _movement;
    public UnitAttack Attack => _attack;
    public UnitConfig UnitConfig => _config;
    public Enemy Target => _target;
    public Character Character => _character;
    public FXUnit FXUnit => _fxUnit;

    private void Awake()
    {
        _maxValue = _config.Health;
        _value = _maxValue;

        _movement = GetComponent<UnitMovement>();
        _attack = GetComponent<UnitAttack>();
        _unitAnimator = GetComponent<UnitAnimator>();
        _searchTarget = GetComponent<UnitSearchTarget>();
        _controller = GetComponent<UnitObserver>();
        _fxUnit = GetComponent<FXUnit>();
    }

    private void OnEnable()
    {
        if (_soundSpawn != null)
            _soundSpawn.Play();
    }

    public void ClearTarget() => _target = null;

    public void SetTarget(Enemy target)
    {
        if (_target != null)
            _target.Diying -= ClearTarget;

        _target = target;
        _target.Diying += ClearTarget;
    }

    public void SetCharacter(Character character)
    {
        _character = character;
    }

    public override void TakeDamage(int damage)
    {

        if (_policeArmour != null)
        {
            BlockDamageWithArmour(damage);

            if (_policeArmour.Value > 0)
                return;
        }

        base.TakeDamage(damage);

        if (_value <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        Died?.Invoke();
        var delay = new WaitForSeconds(_delayBetweenDeath);

        var zombieConverter = GetComponent<SkillEmo>();

        if (zombieConverter != null)
        {
            zombieConverter.ConvertEnemyToAlly(_lastAttacker, _character);
        }

        _controller.DisableStates();
        _unitAnimator.PlauDiyingAnimation();
        yield return delay;

        Destroy(gameObject);
    }

    private void BlockDamageWithArmour(int damage)
    {
        _policeArmour.TakeDamage(damage);
    }
}