using UnityEngine;

public class ZombieView : CreatureView
{
    public Animator Animator => _animator;

    private float _startSpeedAnimation;

    private void Awake()
    {
        _startSpeedAnimation = _animator.speed;
    }

    public override void Initialize() => _animator = GetComponent<Animator>();

    public override void StartState(string state) => _animator.SetBool(state, true);
    public override void StopState(string state) => _animator.SetBool(state, false);

    public void ChangeSpeed (float speed) =>_animator.speed /= speed;
    public void RestoreAnimationSpeed() =>_animator.speed = _startSpeedAnimation;
}
