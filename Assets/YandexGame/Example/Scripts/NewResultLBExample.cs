using UnityEngine;

namespace YG.Example
{
    public class NewResultLBExample : MonoBehaviour
    {
        [SerializeField] private LeaderboardYG _leaderboardYG;
        [SerializeField] private SaverTest _saverTest;

        private void OnEnable()
        {
            _saverTest.LoadedData += NewScore;
        }

        private void OnDisable()
        {
            _saverTest.LoadedData += NewScore;
        }

        public void NewScore(int score)
        {
            YandexGame.NewLeaderboardScores(_leaderboardYG.nameLB, score);
        }
    }
}