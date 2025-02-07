using UI;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Advertisement
{
    public class VideoAd : MonoBehaviour
    {
        [SerializeField] private Buyer _buyer;
        [SerializeField] private RewardView _rewardView;

        public event UnityAction RewardedCallback;

        private void OnEnable()
        {
            YandexGame.OpenVideoEvent += OnOpenCallback;
            YandexGame.CloseVideoEvent += OnCloseCallback;
            YandexGame.RewardVideoEvent += OnRewardCallback;
            YandexGame.RewardVideoEvent += Rewarded;
        }

        private void OnDisable()
        {
            YandexGame.OpenVideoEvent -= OnOpenCallback;
            YandexGame.CloseVideoEvent -= OnCloseCallback;
            YandexGame.RewardVideoEvent -= OnRewardCallback;
            YandexGame.RewardVideoEvent -= Rewarded;
        }

        public void Show()
        {
            YandexGame.RewVideoShow(0);
        }

        public void Rewarded(int id)
        {
            if (id == 1)
            {
                _buyer.GetMoney((int)_rewardView.CountReward);
            }
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            AudioListener.volume = 0f;
        }

        private void OnCloseCallback()
        {
            Time.timeScale = 0;
            AudioListener.volume = 1f;
        }

        private void OnRewardCallback(int reward)
        {
            RewardedCallback?.Invoke();
        }
    }
}