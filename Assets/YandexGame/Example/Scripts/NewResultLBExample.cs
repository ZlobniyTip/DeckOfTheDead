using UnityEngine;

namespace YG.Example
{
    public class NewResultLBExample : MonoBehaviour
    {
        [SerializeField] private LeaderboardYG _leaderboardYG;
        [SerializeField] private SaverTest _saverTest;

        private void OnEnable()
        {
            _saverTest.LoadedLeaderboardScore += NewScore;
            _saverTest.SavedLeaderboardScore += NewScore;
        }

        private void OnDisable()
        {
            _saverTest.LoadedLeaderboardScore -= NewScore;
            _saverTest.SavedLeaderboardScore -= NewScore;
        }

        private void NewScore(int score)
        {
            YandexGame.NewLeaderboardScores(_leaderboardYG.nameLB, score);
        }
    }
}