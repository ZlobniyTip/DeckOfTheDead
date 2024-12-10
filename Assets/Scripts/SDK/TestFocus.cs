using UnityEngine;
using YG;

public class TestFocus : MonoBehaviour
{
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
            if (PlayerPrefs.HasKey("currentVolume"))
            {
                AudioListener.volume = PlayerPrefs.GetFloat("currentVolume");
                return;
            }

            AudioListener.volume = 1;
        }

        if (value == true)
        {
            AudioListener.volume = 0;
        }
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
