using UnityEngine;

public class RewardCounter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _cardUnit;
    [SerializeField] private Character _character;
    [SerializeField] private CharacterShooting _characterShooting;

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
    }

    private void OnDisable()
    {
        _cardUnit.UsedCard -= AddCardUsed;
        _characterShooting.CausedDamage -= AddHeroDamage;
        _character.ChangedLeaderboardScore -= AddLeaderboardScore;
        _characterShooting.KilledTarget -= AddKilledZombie;
    }

    private void AddCardUsed() => UnitsUsed++;
    private void AddHeroDamage(int damage) => HeroDamage += damage;
    private void AddLeaderboardScore(int score) => LeaderboardScore += score;
    private void AddKilledZombie() => KilledEnemies++;
}