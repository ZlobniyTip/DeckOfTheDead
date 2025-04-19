using System.Collections.Generic;
using System.Linq;
using Character.StateMachine.States;

namespace Character.StateMachine
{
    public class CharacterStateMachine : IStateSwitcher
    {
        private readonly List<IState> _states;

        private IState _currentState;

        public CharacterStateMachine(Player character)
        {
            _states = new List<IState>()
        {
            new IdlingState(this, character),
            new RunningState(this, character),
            new AttackState(this, character),
        };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<TState>() 
            where TState : IState
        {
            IState state = _states.FirstOrDefault(state => state is TState);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();
    }
}