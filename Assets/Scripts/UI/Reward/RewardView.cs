using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reward
{
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private GameObject _buttonsPanel;
        [SerializeField] private Button _activPanelButton;

        [SerializeField] private RewardCounter _rewardCounter;
        [SerializeField] private TMP_Text _heroDamage;
        [SerializeField] private TMP_Text _leaderboardScore;
        [SerializeField] private TMP_Text _unitsUsed;
        [SerializeField] private TMP_Text _killedEnemies;
        [SerializeField] private TMP_Text _reward;

        private float _delayCouner = 0.005f;
        private int _counterStep = 1000;
        private int _completedCoroutines = 0;

        private float _countRewardHeroDamage = 0;
        private float _countRewardLbScore = 0;
        private float _countRewardUnitsUsed = 0;
        private float _countRewardKilledEnemies = 0;

        public event Action<float> RewardCounted;

        public float CountReward { get; private set; } = 0;

        private void Start()
        {
            _gamePanel.SetActive(false);
            _activPanelButton.onClick.AddListener(OnActivPanel);
            StartCoroutine(ChangeValue(_rewardCounter.HeroDamage, _heroDamage, 10, _countRewardHeroDamage));
            StartCoroutine(ChangeValue(_rewardCounter.LeaderboardScore, _leaderboardScore, 0.05f, _countRewardLbScore));
            StartCoroutine(ChangeValue(_rewardCounter.UnitsUsed, _unitsUsed, 100, _countRewardUnitsUsed));
            StartCoroutine(ChangeValue(_rewardCounter.KilledEnemies, _killedEnemies, 100, _countRewardKilledEnemies));
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