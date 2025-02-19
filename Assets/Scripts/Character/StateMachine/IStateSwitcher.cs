using Character.StateMachine.States;

namespace Character.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<State>() where State : IState;
    }
}