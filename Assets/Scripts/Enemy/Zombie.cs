using Enemy.StateMachine;
using Enemy.StateMachine.State;
using Other;
using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(ZombieSearchTarget))]
    [RequireComponent(typeof(EnemyMovement))]
    public class Zombie : Health
    {
        private readonly float DelayBetweenDeath = 2.5f;

        [SerializeField] private ZombieView _zombieView;
        [SerializeField] private ParticleSystem _effectCamp;
        [SerializeField] private int _rewardLeaderboardPoints;

        private ZombieAttack _zombieAttack;
        private EnemyMovement _movement;
        private ZombieStateMachine _zombieStateMachine;
        private ZombieSearchTarget _zombieSearch;
        private bool _isUnderCamp = false;
        private bool _isIgnored = false;

        public bool IsIgnored => _isIgnored;

        public int Reward => _rewardLeaderboardPoints;

        public bool IsUnderCamp => _isUnderCamp;

        public ZombieSearchTarget ZombieSearch => _zombieSearch;

        public EnemyMovement Movement => _movement;

        public ZombieView ZombieView => _zombieView;

        public ZombieAttack ZombieAttack => _zombieAttack;


        public event Action<int> DieRewarder;

        private void Awake()
        {
            _zombieSearch = GetComponent<ZombieSearchTarget>();
            _zombieAttack = GetComponent<ZombieAttack>();
            _zombieView.Initialize();
            _movement = GetComponent<EnemyMovement>();
            _zombieStateMachine = new ZombieStateMachine(this);

            Value = MaxValue;
            _effectCamp.Stop();
        }

        private void Update()
        {
            _zombieStateMachine.Update();
        }

        public void SetIgnoredStatus(bool ignored)
        {
            _isIgnored = ignored;
        }

        public void EnterCamp()
        {
            if (_effectCamp != null)
                _effectCamp.Play();

            _isUnderCamp = true;
        }

        public void ExitCamp()
        {
            if (_effectCamp != null)
                _effectCamp.Stop();

            _isUnderCamp = false;
        }

        public override void TakeHeal(int healValue)
        {
            if (IsDiying == false)
                base.TakeHeal(healValue);
        }

        public override void TakeDamage(int damage)
        {
            if (IsDiying)
                return;

            base.TakeDamage(damage);

            if (Value <= 0)
                StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            if (IsDiying) 
                yield break;

            DieRewarder?.Invoke(_rewardLeaderboardPoints);
            DeclareDeath();
            SetDiyingStatus(true);
            _movement.StopMovement();

            var delay = new WaitForSeconds(DelayBetweenDeath);
            yield return delay;
            Destroy(gameObject);
        }
    }
}