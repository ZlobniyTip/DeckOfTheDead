using System.Collections.Generic;
using System.Linq;

public class UnitStateMachine : IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public UnitStateMachine(Unit unit)
    {
        _states = new List<IState>()
        {
             new UnitIdlingState(this, unit),
            new UnitRunningState(this, unit),
            new UnitAttackState(this, unit),
            new UnitDiyingState(this, unit)
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
