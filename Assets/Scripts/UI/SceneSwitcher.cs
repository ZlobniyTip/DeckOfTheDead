using Advertisement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG.Example;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _repeatLevelButton;
    [SerializeField] private SaverTest _saver;
    [SerializeField] private VideoAd _videoAd;

    private int _indexCurrentScene;

    private void OnEnable()
    {
        _indexCurrentScene = SceneManager.GetActiveScene().buildIndex;

        if (_nextLevelButton != null)
        {
            _nextLevelButton.onClick.AddListener(EnableNextLevel);
            _nextLevelButton.onClick.AddListener(_saver.Save);
            _nextLevelButton.onClick.AddListener(_videoAd.Show);
        }
        else
        {
            _repeatLevelButton.onClick.AddListener(RepeatLevel);
        }
    }

    private void OnDisable()
    {
        if (_nextLevelButton != null)
        {
            _nextLevelButton.onClick.RemoveListener(EnableNextLevel);
            _nextLevelButton.onClick.RemoveListener(_saver.Save);
            _nextLevelButton.onClick.RemoveListener(_videoAd.Show);
        }
        else
        {
            _repeatLevelButton.onClick.RemoveListener(RepeatLevel);
        }
    }

    private void EnableNextLevel()
    {
        SceneManager.LoadScene(_indexCurrentScene + 1);
    }

    private void RepeatLevel()
    {
        SceneManager.LoadScene(_indexCurrentScene);
    }
}