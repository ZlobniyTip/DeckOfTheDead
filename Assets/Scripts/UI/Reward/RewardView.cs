using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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
        private int _counterStep = 50;
        private int _completedCoroutines = 0;

        private int _countReward = 0;

        public event Action<int> RewardCounted;

        private void Start()
        {
            _gamePanel.SetActive(false);
            _activPanelButton.onClick.AddListener(OnActivPanel);
            StartCoroutine(ChangeValue(_rewardCounter.HeroDamage, _heroDamage));
            StartCoroutine(ChangeValue(_rewardCounter.LeaderboardScore, _leaderboardScore));
            StartCoroutine(ChangeValue(_rewardCounter.UnitsUsed, _unitsUsed));
            StartCoroutine(ChangeValue(_rewardCounter.KilledEnemies, _killedEnemies));
        }

        private void OnEnable()
        {
            _activPanelButton.onClick.RemoveListener(OnActivPanel);
        }

        private IEnumerator ChangeValue(int value, TMP_Text text)
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
            _countReward += value;

            if (counter > value)
                text.text = value.ToString();

            if (_completedCoroutines == 4)
                StartCoroutine(ChangeValue(_countReward / 2, _reward));
        }

        private void OnActivPanel()
        {
            RewardCounted?.Invoke(_countReward);
            _buttonsPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}