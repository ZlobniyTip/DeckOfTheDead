using UnityEngine;

namespace Other
{
    [RequireComponent(typeof(Animator))]
    public abstract class CreatureView : MonoBehaviour
    {
        protected Animator Animator { get; set; }

        public virtual void Initialize() => Animator = GetComponent<Animator>();

        public virtual void StartState(string state) => Animator.SetBool(state, true);

        public virtual void StopState(string state) => Animator.SetBool(state, false);
    }
}