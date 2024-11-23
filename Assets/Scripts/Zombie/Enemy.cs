using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : Health
{
    [SerializeField] private ZombieView _zombieView;

    public Health _target;
    private ZombieAttack _zombieAttack;
    private EnemyMovement _movement;
    private ZombieStateMachine _zombieStateMachine;
    private Health _startTarget;

    private float _delayBetweenDeath = 2.5f;

    public event Action Diying;

    public bool IsDiying { get; private set; } = false;
    public EnemyMovement Movement => _movement;
    public Health Target => _target;
    public ZombieView ZombieView => _zombieView;
    public ZombieAttack ZombieAttack => _zombieAttack;

    private void Awake()
    {
        _zombieAttack = GetComponent<ZombieAttack>();
        _zombieView.Initialize();
        _movement = GetComponent<EnemyMovement>();
        _zombieStateMachine = new ZombieStateMachine(this);

        _value = _maxValue;
    }

    private void Update()
    {
        _zombieStateMachine.Update();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (_value <= 0)
        {
            IsDiying = true;
            Diying?.Invoke();
            StartCoroutine(Die());
        }
    }

    public void InitializeTarget(Health target)
    {
        _target = target;
        _startTarget = target;
    }

    private IEnumerator Die()
    {
        _movement.StopMovement();

        var delay = new WaitForSeconds(_delayBetweenDeath);

        yield return delay;

        Destroy(gameObject);
    }

    public void SetStartTarget()
    {
        _target = _startTarget;
    }
}
