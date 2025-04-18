using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reward
{
    public class RewardView : MonoBehaviour
    {
        private readonly float _countRewardHeroDamage = 0;
        private readonly float _countRewardLbScore = 0;
        private readonly float _countRewardUnitsUsed = 0;
        private readonly float _countRewardKilledEnemies = 0;
        private readonly float _delayCouner = 0.005f;
        private readonly float _damagRewardCoefficient = 10f;
        private readonly float _rewardIncreaseCoefficientLeaderboard = 0.05f;
        private readonly float _unitRewardIncreaseRate = 100f;
        private readonly float _coefficientIncreasingRewardKilledEnemies = 100f;
        private readonly int _counterStep = 1000;
        private readonly int _totalCoroutines = 4;
        private readonly int _rewardCoroutineIndex = 5;
        private readonly float _finalRewardMultiplier = 0.3f;

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

        public event Action<float> RewardCounted;

        public float CountReward { get; private set; } = 0;

        private void Start()
        {
            _gamePanel.SetActive(false);
            _activPanelButton.onClick.AddListener(OnActivPanel);
            StartCoroutine(ChangeValue(_rewardCounter.HeroDamage, _heroDamage, 
                _damagRewardCoefficient, _countRewardHeroDamage));

            StartCoroutine(ChangeValue(_rewardCounter.LeaderboardScore, _leaderboardScore, 
                _rewardIncreaseCoefficientLeaderboard, _countRewardLbScore));

            StartCoroutine(ChangeValue(_rewardCounter.UnitsUsed, _unitsUsed,
                _unitRewardIncreaseRate, _countRewardUnitsUsed));

            StartCoroutine(ChangeValue(_rewardCounter.KilledEnemies, _killedEnemies, 
                _coefficientIncreasingRewardKilledEnemies, _countRewardKilledEnemies));
        }

        private void OnEnable()
        {
            _activPanelButton.onClick.RemoveListener(OnActivPanel);
        }

        private IEnumerator ChangeValue(float value, TMP_Text text, float multiply, float countReward)
        {
            var delay = new WaitForSeconds(_delayCouner);
            int counter = 0;

            while (counter < value)
            {
                counter += _counterStep;
                text.text = counter.ToString();

                yield return delay;
            }

            _completedCoroutines++;

            if (_completedCoroutines != _rewardCoroutineIndex)
            {
                countReward += value * multiply;
                CountReward += countReward;
            }

            if (counter > value)
                text.text = value.ToString();

            if (_completedCoroutines == _totalCoroutines)
            {
                StartCoroutine(ChangeValue(CountReward, _reward, 
                    _finalRewardMultiplier, CountReward));
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