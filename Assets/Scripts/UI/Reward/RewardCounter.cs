using Character;
using Spawner;
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
            _cardUnit.UsedCard += AddCardUsed;
            _characterShooting.CausedDamage += AddHeroDamage;
            _character.ChangedLeaderboardScore += AddLeaderboardScore;
            _characterShooting.KilledTarget += AddKilledZombie;
            _spawner.ZombieDie += AddKilledZombie;
        }

        private void OnDisable()
        {
            _cardUnit.UsedCard -= AddCardUsed;
            _characterShooting.CausedDamage -= AddHeroDamage;
            _character.ChangedLeaderboardScore -= AddLeaderboardScore;
            _characterShooting.KilledTarget -= AddKilledZombie;
            _spawner.ZombieDie -= AddKilledZombie;
        }

        private void AddCardUsed() => UnitsUsed++;

        private void AddHeroDamage(int damage) => HeroDamage += damage;

        private void AddLeaderboardScore(int score) => LeaderboardScore += score;

        private void AddKilledZombie() => KilledEnemies++;
    }
}