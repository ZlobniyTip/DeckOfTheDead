using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ZombieSearchTarget))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : Health
{
    [SerializeField] private ZombieView _zombieView;

    private ZombieAttack _zombieAttack;
    private EnemyMovement _movement;
    private ZombieStateMachine _zombieStateMachine;
    private ZombieSearchTarget _zombieSearch;

    private float _delayBetweenDeath = 2.5f;

    public event Action Diying;
    public event Action<Enemy> Died;

    public bool IsDiying { get; private set; } = false;

    public ZombieSearchTarget ZombieSearch => _zombieSearch;
    public EnemyMovement Movement => _movement;
    public ZombieView ZombieView => _zombieView;
    public ZombieAttack ZombieAttack => _zombieAttack;

    private void Awake()
    {
        _zombieSearch = GetComponent<ZombieSearchTarget>();
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
            Died?.Invoke(this);
            IsDiying = true;
            Diying?.Invoke();
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        _movement.StopMovement();

        var delay = new WaitForSeconds(_delayBetweenDeath);

        yield return delay;

        Destroy(gameObject);
    }
}