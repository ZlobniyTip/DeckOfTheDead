using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitStateMachine : IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public UnitStateMachine(Character character)
    {
        _states = new List<IState>()
        {

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
