using UnityEngine;

namespace Units.Skills
{
    public class BikerRage : Skill
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;

        private int _impactCounter = 0;
        private int _criticalAttackCounter = 4;
        private int _multiplyDamage = 2;

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
            _unit.Target.TakeDamage(_unit.Attack.CurrentWeapon.Damage * _multiplyDamage);
        }

        private void CountStrokes()
        {
            _impactCounter++;

            if (_impactCounter == _criticalAttackCounter)
            {
                UseSkill();
                _impactCounter = 0;
            }
        }
    }
}