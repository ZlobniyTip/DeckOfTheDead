using UnityEngine;

namespace Units.Skills
{
    public class BikerRage : Skill
    {
        private readonly int _criticalAttackCounter = 4;
        private readonly int _multiplyDamage = 2;

        [SerializeField] private Unit _unit;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;

        private int _impactCounter = 0;

        private void Start()
        {
            _unit.Attack.CurrentWeapon.Shooting += OnCountStrokes;
        }

        private void OnDestroy()
        {
            _unit.Attack.CurrentWeapon.Shooting -= OnCountStrokes;
        }

        public override void OnUseSkill()
        {
            _particleSystem.Play();
            _audioSource.Play();
            _unit.Target.TakeDamage(_unit.Attack.CurrentWeapon.DamageValue * _multiplyDamage);
        }

        private void OnCountStrokes()
        {
            _impactCounter++;

            if (_impactCounter == _criticalAttackCounter)
            {
                OnUseSkill();
                _impactCounter = 0;
            }
        }
    }
}