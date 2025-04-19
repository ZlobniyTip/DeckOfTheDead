using Character.StateMachine;

namespace Enemy.StateMachine.State
{
    public class ZombieAttackState : ZombieMovementState
    {
        private const string _attackingZombie = "IsAttacking";

        public ZombieAttackState(IStateSwitcher stateSwitcher, Zombie enemy) 
            : base(stateSwitcher, enemy) { }

        public override void Enter()
        {
            base.Enter();

            ZombieView.StartState(_attackingZombie);
        }

        public override void Exit()
        {
            base.Exit();

            ZombieView.StopState(_attackingZombie);
        }

        public override void Update()
        {
            base.Update();

            if (IsDiying)
            {
                StateSwitcher.SwitchState<ZombieDyingState>();
            }

            if (IsAttacking == false)
            {
                StateSwitcher.SwitchState<ZombieRunningState>();
            }
        }
    }
}