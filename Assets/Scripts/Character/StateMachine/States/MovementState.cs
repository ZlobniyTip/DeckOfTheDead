using UnityEngine;

public class MovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;

    private readonly Character _character;

    public MovementState(IStateSwitcher stateSwitcher, Character character)
    {
        StateSwitcher = stateSwitcher;
        _character = character;
    }

    protected CharacterView CharacterView => _character.CharacterView;
    protected Character Character => _character;

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }

    protected bool IsMoving() => Character.Movement.NavMeshAgent.speed == 0;
    protected bool IsAttacking() => Character.CharacterShooting.IsShooting;
}