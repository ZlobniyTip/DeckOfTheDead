using System.Collections.Generic;
using System.Linq;

public class ZombieStateMachine : IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public ZombieStateMachine(Enemy enemy)
    {
        _states = new List<IState>()
        {
            new ZombieIdlingState(this, enemy),
            new ZombieRunningState(this, enemy),
            new ZombieAttackState(this, enemy)
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<State>() where State : IState
    {
        IState state = _states.FirstOrDefault(state => state is State);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Update() => _currentState.Update();
}
