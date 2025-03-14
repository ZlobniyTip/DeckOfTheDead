using Character.StateMachine;

namespace Enemy.StateMachine.State
{
    public class ZombieRunningState : ZombieMovementState
    {
        private const string Running = "IsRunning";

        public ZombieRunningState(IStateSwitcher stateSwitcher, Zombie enemy) 
            : base(stateSwitcher, enemy) { }

        public override void Enter()
        {
            base.Enter();

            ZombieView.StartState(Running);
        }

        public override void Exit()
        {
            base.Exit();

            ZombieView.StopState(Running);
        }

        public override void Update()
        {
            base.Update();

            if (IsAttacking)
            {
                StateSwitcher.SwitchState<ZombieAttackState>();
                return;
            }

            if (IsDiying)
                StateSwitcher.SwitchState<ZombieDyingState>();
        }
    }
}