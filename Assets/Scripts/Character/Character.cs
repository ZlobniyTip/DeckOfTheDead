using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterShooting))]
[RequireComponent(typeof(PlayerEnergy))]
public class Character : Health
{
    [SerializeField] private CharacterView _characterView;

    private CharacterShooting _characterShooting;
    private CharacterMovement _movement;
    private CharacterCards _characterCards;
    private PlayerEnergy _playerEnergy;

    private CharacterStateMachine _stateMachine;

    public PlayerEnergy Energy => _playerEnergy;
    public CharacterMovement Movement => _movement;
    public CharacterView CharacterView => _characterView;
    public CharacterShooting CharacterShooting => _characterShooting;

    private void Awake()
    {
        _playerEnergy = GetComponent<PlayerEnergy>();
        _characterShooting = GetComponent<CharacterShooting>();
        _characterView.Initialize();
        _movement = GetComponent<CharacterMovement>();
        _stateMachine = new CharacterStateMachine(this);

        _value = _maxValue;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (_value <= 0)
        {
            Destroy(gameObject);
        }
    }
}