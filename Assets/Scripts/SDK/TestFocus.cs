using UI;
using UnityEngine;
using YG;

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

    private void PauseGame(bool value)
    {
        if (_menu.IsPaused)
            return;

        Time.timeScale = value ? 0 : 1;
    }
}
