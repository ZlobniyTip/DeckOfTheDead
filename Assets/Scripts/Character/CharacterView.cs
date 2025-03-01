using Other;
using UnityEngine;

namespace Character
{
    public class CharacterView : CreatureView
    {
        public override void Initialize() => Animator = GetComponent<Animator>();

        public override void StartState(string state) => Animator.SetBool(state, true);

        public override void StopState(string state) => Animator.SetBool(state, false);
    }
}