using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ZombieSearchTarget))]
[RequireComponent(typeof(EnemyMovement))]
public class Zombie : Health
{
    [SerializeField] private ZombieView _zombieView;
    [SerializeField] private ParticleSystem _effectCamp;
    [SerializeField] private int _rewardLeaderboardPoints;

    private ZombieAttack _zombieAttack;
    private EnemyMovement _movement;
    private ZombieStateMachine _zombieStateMachine;
    private ZombieSearchTarget _zombieSearch;
    private bool _isUnderCamp = false;
    private float _delayBetweenDeath = 2.5f;

    public int Reward => _rewardLeaderboardPoints;
    public bool IsUnderCamp => _isUnderCamp;
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
        _effectCamp.Stop();
    }

    private void Update()
    {
        _zombieStateMachine.Update();
    }

    public void EnterCamp()
    {
        if (_effectCamp != null)
            _effectCamp.Play();

        _isUnderCamp = true;
    }

    public void ExitCamp()
    {
        if (_effectCamp != null)
            _effectCamp.Stop();

        _isUnderCamp = false;
    }

    public override void TakeHeal(int healValue)
    {
        if (IsDiying == false)
        base.TakeHeal(healValue);
    }

    public override void TakeDamage(int damage)
    {
        if (IsDiying)
            return;

        base.TakeDamage(damage);

        if (_value <= 0)
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        if (IsDiying) yield break;

        DeclareDeath();
        SetDiyingStatus(true);
        _movement.StopMovement();

        var delay = new WaitForSeconds(_delayBetweenDeath);
        yield return delay;
        Destroy(gameObject);
    }
}