using UI;
using UnityEngine;
using YG;

namespace SDK
{
    public class TestFocus : MonoBehaviour
    {
        [SerializeField] private Menu _menu;

        private void OnEnable()
        {
            YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;
        }

        private void OnDisable()
        {
            YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
        }

        private void OnVisibilityWindowGame(bool inApp)
        {
            MuteAudio(!inApp);
            PauseGame(!inApp);
        }

        private void MuteAudio(bool value)
        {
            if (value == false)
            {
                AudioListener.volume = YandexGame.savesData.sound;
            }

            if (value == true)
            {
                AudioListener.volume = 0;
            }
        }

        public void PauseGame(bool value)
        {
            if (_menu.IsPaused)
                return;

            Time.timeScale = value ? 0 : 1;
        }
    }
}