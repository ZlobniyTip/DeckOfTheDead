using System.Collections;
using UnityEngine;

public class Unit : Health
{
    [SerializeField] private UnitConfig _config;
    [SerializeField] private AudioSource _soundSpawn;

    private Character _character;
    private Enemy _target;
    private UnitMovement _movement;
    private UnitAttack _attack;
    private UnitSearchTarget _searchTarget;
    private UnitAnimator _unitAnimator;
    private UnitObserver _controller;
    private float _delayBetweenDeath = 2.7f;

    public UnitMovement Movement => _movement;
    public UnitAttack Attack => _attack;
    public UnitConfig UnitConfig => _config;
    public Enemy Target => _target;
    public Character Character => _character;

    private void Awake()
    {
        _maxValue = _config.Health;
        _value = _maxValue;

        _movement = GetComponent<UnitMovement>();
        _attack = GetComponent<UnitAttack>();
        _unitAnimator = GetComponent<UnitAnimator>();
        _searchTarget = GetComponent<UnitSearchTarget>();
        _controller = GetComponent<UnitObserver>();
    }

    private void OnEnable()
    {
        if (_soundSpawn != null)
            _soundSpawn.Play();
    }

    public void ClearTarget()
    {
        _target = null;
    }

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
        base.TakeDamage(damage);

        if (_value <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        var delay = new WaitForSeconds(_delayBetweenDeath);

        _controller.DisableStates();
        _unitAnimator.PlauDiyingAnimation();
        yield return delay;

        Destroy(gameObject);
    }
}