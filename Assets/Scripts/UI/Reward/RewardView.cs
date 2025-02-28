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
        private readonly int CounterStep = 1000;

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
            StartCoroutine(ChangeValue(_rewardCounter.HeroDamage, _heroDamage, 10, CountRewardHeroDamage));
            StartCoroutine(ChangeValue(_rewardCounter.LeaderboardScore, _leaderboardScore, 0.05f, CountRewardLbScore));
            StartCoroutine(ChangeValue(_rewardCounter.UnitsUsed, _unitsUsed, 100, CountRewardUnitsUsed));
            StartCoroutine(ChangeValue(_rewardCounter.KilledEnemies, _killedEnemies, 100, CountRewardKilledEnemies));
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

            if (_completedCoroutines != 5)
            {
                countReward += value * multiply;
                CountReward += countReward;
            }

            if (counter > value)
                text.text = value.ToString();

            if (_completedCoroutines == 4)
            {
                StartCoroutine(ChangeValue(CountReward, _reward, 0.3f, CountReward));
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