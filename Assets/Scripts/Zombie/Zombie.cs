using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ZombieSearchTarget))]
[RequireComponent(typeof(ZombieMovement))]
public class Zombie : Health
{
    [SerializeField] private ZombieView _zombieView;

    private ZombieAttack _zombieAttack;
    private ZombieMovement _movement;
    private ZombieStateMachine _zombieStateMachine;
    private ZombieSearchTarget _zombieSearch;
    private FXZombie _fxZombie;
    private bool _isUnderCamp = false;
    private float _delayBetweenDeath = 2.5f;

    public event Action Diying;

    public bool IsDiying { get; private set; } = false;
    public bool IsUnderCamp => _isUnderCamp;
    public ZombieSearchTarget ZombieSearch => _zombieSearch;
    public ZombieMovement Movement => _movement;
    public ZombieView ZombieView => _zombieView;
    public ZombieAttack ZombieAttack => _zombieAttack;

    private void Awake()
    {
        _zombieSearch = GetComponent<ZombieSearchTarget>();
        _zombieAttack = GetComponent<ZombieAttack>();
        _zombieView.Initialize();
        _movement = GetComponent<ZombieMovement>();
        _fxZombie = GetComponent<FXZombie>();
        _zombieStateMachine = new ZombieStateMachine(this);

        _value = _maxValue;
     
    }

    private void Update()
    {
        _zombieStateMachine.Update();
    }

    public void EnterCamp()
    {
       _fxZombie.EnterCamp();
        _isUnderCamp = true;
    }

    public void ExitCamp()
    {
        _fxZombie.ExitCamp();
        _isUnderCamp = false;
    } 

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (_value <= 0)
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        IsDiying = true;
        Diying?.Invoke();
        _movement.StopMovement();

        var delay = new WaitForSeconds(_delayBetweenDeath);
        yield return delay;
        Destroy(gameObject);
    }
}