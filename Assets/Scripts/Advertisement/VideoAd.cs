using Character;
using UI.Reward;
using UnityEngine;
using YG;
using YG.Example;

namespace Advertisement
{
    public class VideoAd : MonoBehaviour
    {
        private readonly int AmountReward = 5000;

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
            YandexGame.RewVideoShow(0);
        }

        public void GivePlayerMoney()
        {
            YandexGame.RewVideoShow(1);
        }

        public void Rewarded(int id)
        {
            if (id == 0)
            {
                _buyer.GetMoney((int)_rewardView.CountReward);
            }
            else if (id == 1)
            {
                _buyer.GetMoney(AmountReward);
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