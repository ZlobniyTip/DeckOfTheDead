using Character.StateMachine;
using UnityEngine;

namespace Enemy.StateMachine.State
{
    public class ZombieDyingState : ZombieMovementState
    {
        private const string DiyingVar1 = "IsDiyingVar1";
        private const string DiyingVar2 = "IsDiyingVar2";

        private int _randomState;

        public ZombieDyingState(IStateSwitcher stateSwitcher, Zombie enemy) 
            : base(stateSwitcher, enemy) { }

        public override void Enter()
        {
            base.Enter();

            _randomState = Random.Range(0, 2);

            if (_randomState == 0)
            {
                ZombieView.StartState(DiyingVar1);
            }
            else
            {
                ZombieView.StartState(DiyingVar2);
            }
        }

        public override void Exit()
        {
            base.Exit();

            if (_randomState == 0)
            {
                ZombieView.StopState(DiyingVar1);
            }
            else
            {
                ZombieView.StopState(DiyingVar2);
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}