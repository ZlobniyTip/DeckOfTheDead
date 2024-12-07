using System.Collections;
using UnityEngine;

public class Unit : Health
{
    [SerializeField] private UnitConfig _config;
    [SerializeField] private UnitView _view;

    private Character _character;
    private Enemy _target;
    private float _delayBetweenDeath = 2.5f;
    private UnitMovement _movement;
    private UnitAttack _attack;
    private UnitStateMachine _stateMachine;
    private UnitSearchTarget _searchTarget;
    private UnitAnimator _unitAnimator;

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
        _unitAnimator = GetComponent<UnitAnimator>();
        _searchTarget = GetComponent<UnitSearchTarget>();
        _view.Initialize();   
    }

    private void Start()
    {
        _stateMachine = new UnitStateMachine(this);
    }

    private void Update()
    {
        _stateMachine.Update();
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

        _movement.enabled = false;
        _attack.StopAttack();
        _searchTarget.StopSearch();
        _unitAnimator.PlauDiyingAnimation();
        yield return delay;

        Destroy(gameObject);
    }
}