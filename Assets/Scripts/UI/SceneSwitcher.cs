using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;

    private int _indexCurrentScene;

    private void OnEnable()
    {
        _indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
        _nextLevelButton.onClick.AddListener(EnableNextLevel);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(EnableNextLevel);
    }

    private void EnableNextLevel()
    {
        SceneManager.LoadScene(_indexCurrentScene + 1);
    }
}