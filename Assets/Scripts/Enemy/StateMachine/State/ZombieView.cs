using Other;
using UnityEngine;

namespace Enemy.StateMachine.State
{
    public class ZombieView : CreatureView
    {
        private float _startSpeedAnimation;

        private void Start()
        {
            _startSpeedAnimation = _animator.speed;
        }

        public override void Initialize() => _animator = GetComponent<Animator>();

        public override void StartState(string state) => _animator.SetBool(state, true);
        public override void StopState(string state) => _animator.SetBool(state, false);

        public void ChangeSpeed(float speed) => _animator.speed /= speed;
        public void RestoreAnimationSpeed() => _animator.speed = _startSpeedAnimation;
    }
}