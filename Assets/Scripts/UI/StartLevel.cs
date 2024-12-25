using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class StartLevel : MonoBehaviour
{
    public void StartCurrentLevel()
    {
        SceneManager.LoadScene(YandexGame.savesData.currentLevel);
    }
}