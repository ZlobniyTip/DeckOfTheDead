using UnityEngine;

public class ZombieView : CreatureView
{
    public override void Initialize() => _animator = GetComponent<Animator>();

    public override void StartState(string state) => _animator.SetBool(state, true);
    public override void StopState(string state) => _animator.SetBool(state, false);
}
