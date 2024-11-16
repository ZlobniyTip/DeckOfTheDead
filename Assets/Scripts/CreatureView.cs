using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CreatureView : MonoBehaviour
{
    protected Animator _animator;

    public virtual void Initialize() => _animator = GetComponent<Animator>();

    public virtual void StartState(string state) => _animator.SetBool(state, true);
    public virtual void StopState(string state) => _animator.SetBool(state, false);
}