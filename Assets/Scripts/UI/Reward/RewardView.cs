using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reward
{
    public class RewardView : MonoBehaviour
    {
        private readonly float CountRewardHeroDamage = 0;
        private readonly float CountRewardLbScore = 0;
        private readonly float CountRewardUnitsUsed = 0;
        private readonly float CountRewardKilledEnemies = 0;
        private readonly float DelayCouner = 0.005f;
        private readonly float DamagRewardCoefficient = 10f;
        private readonly float RewardIncreaseCoefficientLeaderboard = 0.05f;
        private readonly float UnitRewardIncreaseRate = 100f;
        private readonly float CoefficientIncreasingRewardKilledEnemies = 100f;
        private readonly int CounterStep = 1000;
        private readonly int TotalCoroutines = 4;
        private readonly int RewardCoroutineIndex = 5;
        private readonly float FinalRewardMultiplier = 0.3f;

        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private GameObject _buttonsPanel;
        [SerializeField] private Button _activPanelButton;

        [SerializeField] private RewardCounter _rewardCounter;
        [SerializeField] private TMP_Text _heroDamage;
        [SerializeField] private TMP_Text _leaderboardScore;
        [SerializeField] private TMP_Text _unitsUsed;
        [SerializeField] private TMP_Text _killedEnemies;
        [SerializeField] private TMP_Text _reward;

        private int _completedCoroutines = 0;

        public float CountReward { get; private set; } = 0;

        public event Action<float> RewardCounted;

        private void Start()
        {
            _gamePanel.SetActive(false);
            _activPanelButton.onClick.AddListener(OnActivPanel);
            StartCoroutine(ChangeValue(_rewardCounter.HeroDamage, _heroDamage, DamagRewardCoefficient, CountRewardHeroDamage));
            StartCoroutine(ChangeValue(_rewardCounter.LeaderboardScore, _leaderboardScore, RewardIncreaseCoefficientLeaderboard, CountRewardLbScore));
            StartCoroutine(ChangeValue(_rewardCounter.UnitsUsed, _unitsUsed, UnitRewardIncreaseRate, CountRewardUnitsUsed));
            StartCoroutine(ChangeValue(_rewardCounter.KilledEnemies, _killedEnemies, CoefficientIncreasingRewardKilledEnemies, CountRewardKilledEnemies));
        }

        private void OnEnable()
        {
            _activPanelButton.onClick.RemoveListener(OnActivPanel);
        }

        private IEnumerator ChangeValue(float value, TMP_Text text, float multiply, float countReward)
        {
            var delay = new WaitForSeconds(DelayCouner);
            int counter = 0;

            while (counter < value)
            {
                counter += CounterStep;
                text.text = counter.ToString();

                yield return delay;
            }

            _completedCoroutines++;

            if (_completedCoroutines != RewardCoroutineIndex)
            {
                countReward += value * multiply;
                CountReward += countReward;
            }

            if (counter > value)
                text.text = value.ToString();

            if (_completedCoroutines == TotalCoroutines)
            {
                StartCoroutine(ChangeValue(CountReward, _reward, FinalRewardMultiplier, CountReward));
            }
        }

        private void OnActivPanel()
        {
            RewardCounted?.Invoke(CountReward);
            _buttonsPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}