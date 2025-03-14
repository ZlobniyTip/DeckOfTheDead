using Character.StateMachine;
using Character.StateMachine.States;

namespace Enemy.StateMachine.State
{
    public class ZombieMovementState : IState
    {
        private readonly Zombie _enemy;

        protected IStateSwitcher StateSwitcher { get; set; }

        public ZombieMovementState(IStateSwitcher stateSwitcher, Zombie enemy)
        {
            StateSwitcher = stateSwitcher;
            _enemy = enemy;
        }

        protected ZombieView ZombieView => _enemy.ZombieView;
        protected Zombie Enemy => _enemy;
        protected bool IsAttacking => Enemy.ZombieAttack.IsAttacking;
        protected bool IsDiying => Enemy.IsDiying;

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }
    }
}