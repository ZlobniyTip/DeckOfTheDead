using Other;
using UnityEngine;

namespace Enemy.StateMachine.State
{
    public class ZombieView : CreatureView
    {
        private float _startSpeedAnimation;

        private void Start()
        {
            _startSpeedAnimation = Animator.speed;
        }

        public override void Initialize() => Animator = GetComponent<Animator>();

        public override void StartState(string state) => Animator.SetBool(state, true);
        public override void StopState(string state) => Animator.SetBool(state, false);

        public void ChangeSpeed(float speed) => Animator.speed /= speed;
        public void RestoreAnimationSpeed() => Animator.speed = _startSpeedAnimation;
    }
}