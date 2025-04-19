using System;
using Character.StateMachine;
using Other;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(CharacterShooting))]
    [RequireComponent(typeof(CharacterCards))]
    [RequireComponent(typeof(PlayerEnergy))]

    public class Player : Health
    {
        private CharacterView _characterView;
        private CharacterShooting _characterShooting;
        private CharacterMovement _movement;
        private PlayerEnergy _playerEnergy;
        private int _leaderboardScore;
        private int _currentLevelLeaderboardScore;
        private CharacterStateMachine _stateMachine;

        public event Action<int> LeaderboardScoreChanged;

        public int LeaderboardScore => _leaderboardScore;
        public PlayerEnergy Energy => _playerEnergy;
        public CharacterMovement Movement => _movement;
        public CharacterView CharacterView => _characterView;
        public CharacterShooting CharacterShooting => _characterShooting;

        private void Start()
        {
            _characterView = GetComponent<CharacterView>();
            _playerEnergy = GetComponent<PlayerEnergy>();
            _characterShooting = GetComponent<CharacterShooting>();
            _characterView.Initialize();
            _movement = GetComponent<CharacterMovement>();
            _stateMachine = new CharacterStateMachine(this);

            Value = MaxValue;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            if (Value <= 0)
            {
                DeclareDeath();
                Destroy(gameObject);
            }
        }

        public void GetLeaderboardScore(int score)
        {
            _leaderboardScore += score;
            LeaderboardScoreChanged?.Invoke(_leaderboardScore - _currentLevelLeaderboardScore);
        }

        public void LoadScore(int score)
        {
            _leaderboardScore = score;
            _currentLevelLeaderboardScore = score;
        }
    }
}