using System.Collections.Generic;
using System.Linq;
using Character.StateMachine;
using Character.StateMachine.States;
using Enemy.StateMachine.State;

namespace Enemy.StateMachine
{
    public class ZombieStateMachine : IStateSwitcher
    {
        private readonly List<IState> States;

        private IState _currentState;

        public ZombieStateMachine(Zombie enemy)
        {
            States = new List<IState>()
        {
            new ZombieRunningState(this, enemy),
            new ZombieAttackState(this, enemy),
            new ZombieDiyingState(this, enemy),
        };

            _currentState = States[0];
            _currentState.Enter();
        }

        public void SwitchState<TState>() 
            where TState : IState
        {
            IState state = States.FirstOrDefault(state => state is TState);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();
    }
}