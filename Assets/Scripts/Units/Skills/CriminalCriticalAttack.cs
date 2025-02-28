using UnityEngine;

namespace Units.Skills
{
    public class CriminalCriticalAttack : Skill
    {
        private readonly int LethalCount = 1;
        private readonly int Chance = 20;
        private readonly int MultiplyDamage = 100;

        [SerializeField] private Unit _unit;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private AudioSource _audioSource;

        private void Start()
        {
            _unit.Attack.CurrentWeapon.Shooting += TryInflictLethalDamage;
        }

        private void OnDestroy()
        {
            _unit.Attack.CurrentWeapon.Shooting -= TryInflictLethalDamage;
        }

        public override void UseSkill()
        {
            _particle.Play();
            _audioSource.Play();
            _unit.Target.TakeDamage(_unit.Attack.CurrentWeapon.DamageValue * MultiplyDamage);
        }

        private void TryInflictLethalDamage()
        {
            int random = Random.Range(LethalCount, Chance);

            if (random == LethalCount)
                UseSkill();
        }
    }
}