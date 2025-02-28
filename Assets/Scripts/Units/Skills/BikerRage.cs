using UnityEngine;

namespace Units.Skills
{
    public class BikerRage : Skill
    {
        private readonly int CriticalAttackCounter = 4;
        private readonly int MultiplyDamage = 2;

        [SerializeField] private Unit _unit;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;

        private int _impactCounter = 0;

        private void Start()
        {
            _unit.Attack.CurrentWeapon.Shooting += CountStrokes;
        }

        private void OnDestroy()
        {
            _unit.Attack.CurrentWeapon.Shooting -= CountStrokes;
        }

        public override void UseSkill()
        {
            _particleSystem.Play();
            _audioSource.Play();
            _unit.Target.TakeDamage(_unit.Attack.CurrentWeapon.DamageValue * MultiplyDamage);
        }

        private void CountStrokes()
        {
            _impactCounter++;

            if (_impactCounter == CriticalAttackCounter)
            {
                UseSkill();
                _impactCounter = 0;
            }
        }
    }
}