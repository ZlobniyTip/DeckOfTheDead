using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Advertisement
{
    public class VideoAd : MonoBehaviour
    {
        public event UnityAction RewardedCallback;

        private void OnEnable()
        {
            YandexGame.OpenVideoEvent += OnOpenCallback;
            YandexGame.CloseVideoEvent += OnCloseCallback;
            YandexGame.RewardVideoEvent += OnRewardCallback;
        }

        private void OnDisable()
        {
            YandexGame.OpenVideoEvent -= OnOpenCallback;
            YandexGame.CloseVideoEvent -= OnCloseCallback;
            YandexGame.RewardVideoEvent -= OnRewardCallback;
        }

        public void Show()
        {
            YandexGame.RewVideoShow(0);
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            AudioListener.volume = 0f;
        }

        private void OnCloseCallback()
        {
            Time.timeScale = 1;
            AudioListener.volume = 1f;
        }

        private void OnRewardCallback(int reward)
        {
            RewardedCallback?.Invoke();
        }
    }
}