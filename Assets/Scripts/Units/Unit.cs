using System;
using System.Collections;
using Card;
using Character;
using Enemy;
using Other;
using Units.Skills;
using Units.UnitConfi;
using UnityEngine;
using Weapons;

namespace Units
{
    public class Unit : Health, IAim
    {
        private readonly float DelayBetweenDeath = 2.7f;

        [SerializeField] private UnitConfig _config;
        [SerializeField] private AudioSource _soundSpawn;
        [SerializeField] private PoliceAmmunition _policeArmour;

        private Player _character;
        private Zombie _target;
        private UnitAttack _attack;
        private UnitAnimator _unitAnimator;
        private UnitObserver _controller;
        private FXUnit _fxUnit;
        private CardView _cardView;

        public UnitAttack Attack => _attack;

        public UnitConfig UnitConfig => _config;

        public Zombie Target => _target;

        public Player Character => _character;

        public FXUnit FXUnit => _fxUnit;

        public bool IsZombie { get; set; } = false;


        public event Action<Unit> TurnIntoZombie;

        private void Awake()
        {
            MaxValue = _config.Health;
            Value = MaxValue;

            _attack = GetComponent<UnitAttack>();
            _unitAnimator = GetComponent<UnitAnimator>();
            _controller = GetComponent<UnitObserver>();
            _fxUnit = GetComponent<FXUnit>();
        }

        private void OnEnable()
        {
            if (_soundSpawn != null)
                _soundSpawn.Play();
        }

        public void GetCardView(CardView cardView)
        {
            _cardView = cardView;

            SetParameters();
        }

        public void ClearTarget() => _target = null;

        public void SetTarget(Zombie target)
        {
            if (_target != null)
                _target.Died -= ClearTarget;

            _target = target;
            _target.Died += ClearTarget;
        }

        public void SetCharacter(Player character)
        {
            _character = character;
        }

        public override void TakeDamage(int damage)
        {
            if (_policeArmour != null)
            {
                BlockDamageWithArmour(damage);

                if (_policeArmour.ValueHealth > 0)
                    return;
            }

            base.TakeDamage(damage);

            if (Value <= 0)
            {
                StartCoroutine(Die());
            }
        }

        private void SetParameters()
        {
            MaxValue += _cardView.Card.BonusHealth;
            Value = MaxValue;
            Attack.SetAdditionalDamage(_cardView.Card.BonusDamage);
        }

        private IEnumerator Die()
        {
            DeclareDeath();
            var delay = new WaitForSeconds(DelayBetweenDeath);

            var zombieConverter = GetComponent<SkillEmo>();

            if (zombieConverter != null)
            {
                zombieConverter.ConvertEnemyToAlly(LastAttacker, _character);
            }

            _controller.DisableStates();
            _unitAnimator.PlauDiyingAnimation();
            SetDiyingStatus(true);
            yield return delay;

            TurnIntoZombie?.Invoke(this);

            Destroy(gameObject);
        }

        private void BlockDamageWithArmour(int damage)
        {
            _policeArmour.TakeDamage(damage);
        }
    }
}