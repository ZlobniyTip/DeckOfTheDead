using Character;
using UI.Reward;
using UnityEngine;
using YG;
using YG.Example;

namespace Advertisement
{
    public class VideoAd : MonoBehaviour
    {
        private readonly int _amountReward = 5000;
        private readonly int _rewardIndexMoney = 0;
        private readonly int _rewardIndexMoneyx2 = 1;

        [SerializeField] private Buyer _buyer;
        [SerializeField] private RewardView _rewardView;
        [SerializeField] private SaverTest _saverTest;

        private void OnEnable()
        {
            YandexGame.OpenVideoEvent += OnOpenCallback;
            YandexGame.CloseVideoEvent += OnCloseCallback;
            YandexGame.RewardVideoEvent += Rewarded;
        }

        private void OnDisable()
        {
            YandexGame.OpenVideoEvent -= OnOpenCallback;
            YandexGame.CloseVideoEvent -= OnCloseCallback;
            YandexGame.RewardVideoEvent -= Rewarded;
        }

        public void GivePlayerMoneyX2()
        {
            YandexGame.RewVideoShow(_rewardIndexMoneyx2);
        }

        public void GivePlayerMoney()
        {
            YandexGame.RewVideoShow(_rewardIndexMoney);
        }

        public void Rewarded(int id)
        {
            if (id == _rewardIndexMoney)
            {
                _buyer.GetMoney((int)_rewardView.CountReward);
            }
            else if (id == _rewardIndexMoneyx2)
            {
                _buyer.GetMoney(_amountReward);
            }

            YandexGame.ConsumePurchases();
            _saverTest.Save();
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
    }
}