using Character.StateMachine.States;

namespace Character.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<TState>() 
            where TState : IState;
    }
}