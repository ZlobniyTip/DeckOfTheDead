using UnityEngine;

namespace YG.Example
{
    public class NewResultLBExample : MonoBehaviour
    {
        [SerializeField] private LeaderboardYG _leaderboardYG;
        [SerializeField] private SaverTest _saverTest;

        private void OnEnable()
        {
            _saverTest.LoadedLeaderboardScore += OnLoadedNewScore;
            _saverTest.SavedLeaderboardScore += OnLoadedNewScore;
        }

        private void OnDisable()
        {
            _saverTest.LoadedLeaderboardScore -= OnLoadedNewScore;
            _saverTest.SavedLeaderboardScore -= OnLoadedNewScore;
        }

        private void OnLoadedNewScore(int score)
        {
            YandexGame.NewLeaderboardScores(_leaderboardYG.nameLB, score);
        }
    }
}