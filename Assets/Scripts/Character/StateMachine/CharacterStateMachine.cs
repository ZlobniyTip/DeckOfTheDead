using Character.StateMachine.States;
using System.Collections.Generic;
using System.Linq;

namespace Character.StateMachine
{
    public class CharacterStateMachine : IStateSwitcher
    {
        private readonly List<IState> States;

        private IState _currentState;

        public CharacterStateMachine(Player character)
        {
            States = new List<IState>()
        {
            new IdlingState(this, character),
            new RunningState(this, character),
            new AttackState(this, character)
        };

            _currentState = States[0];
            _currentState.Enter();
        }

        public void SwitchState<State>() where State : IState
        {
            IState state = States.FirstOrDefault(state => state is State);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();
    }
}