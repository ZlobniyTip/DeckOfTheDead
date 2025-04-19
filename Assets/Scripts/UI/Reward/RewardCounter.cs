using Spawner;
using Character;
using UnityEngine;

namespace UI.Reward
{
    public class RewardCounter : MonoBehaviour
    {
        [SerializeField] private UnitSpawner _cardUnit;
        [SerializeField] private Player _character;
        [SerializeField] private CharacterShooting _characterShooting;
        [SerializeField] private ZombieSpawner _spawner;

        public int UnitsUsed { get; private set; }
        public int HeroDamage { get; private set; }
        public int LeaderboardScore { get; private set; }
        public int KilledEnemies { get; private set; }

        private void OnEnable()
        {
            _cardUnit.CardUsed += OnAddCardUsed;
            _characterShooting.DamageCaused += OnAddHeroDamage;
            _character.LeaderboardScoreChanged += OnAddLeaderboardScore;
            _characterShooting.TargetKilled += OnAddKilledZombie;
            _spawner.ZombieDied += OnAddKilledZombie;
        }

        private void OnDisable()
        {
            _cardUnit.CardUsed -= OnAddCardUsed;
            _characterShooting.DamageCaused -= OnAddHeroDamage;
            _character.LeaderboardScoreChanged -= OnAddLeaderboardScore;
            _characterShooting.TargetKilled -= OnAddKilledZombie;
            _spawner.ZombieDied -= OnAddKilledZombie;
        }

        private void OnAddCardUsed() => UnitsUsed++;

        private void OnAddHeroDamage(int damage) => HeroDamage += damage;

        private void OnAddLeaderboardScore(int score) => LeaderboardScore += score;

        private void OnAddKilledZombie() => KilledEnemies++;
    }
}