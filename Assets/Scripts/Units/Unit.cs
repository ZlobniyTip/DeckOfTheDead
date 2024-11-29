using System;
using System.Collections;
using UnityEngine;

public class Unit : Health
{
    [SerializeField] private UnitConfig _config;
    [SerializeField] private UnitView _view;

    private Character _character;
    private Enemy _target;
    private float _delayBetweenDeath = 2f;
    private UnitMovement _movement;
    private UnitAttack _attack;
    private UnitStateMachine _stateMachine;

    public UnitMovement Movement => _movement;
    public UnitAttack Attack => _attack;
    public UnitConfig UnitConfig => _config;
    public Enemy Target => _target;
    public Character Character => _character;
    public UnitView View => _view;

    private void Awake()
    {
        _maxValue = _config.Health;
        _value = _maxValue;

        _movement = GetComponent<UnitMovement>();
        _attack = GetComponent<UnitAttack>();
        //_view = GetComponent<UnitView>();
        _view.Initialize();

        _stateMachine = new UnitStateMachine(this);
    }

    private void Update()
    {
        _stateMachine.Update();
        Debug.Log(_movement.NavMeshAgent.speed);
    }

    public void SetTarget(Enemy target)
    {
        _target = target;
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
        yield return delay;

        Destroy(gameObject);
    }
}