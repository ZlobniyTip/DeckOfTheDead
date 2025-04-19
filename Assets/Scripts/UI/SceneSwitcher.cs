using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using YG.Example;

namespace UI
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField, Scene] private string _nextSceneName;
        [SerializeField, Scene] private string _currentSceneName;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _repeatLevelButton;
        [SerializeField] private SaverTest _saver;

        private void OnEnable()
        {
            if (_nextLevelButton != null)
            {
                _nextLevelButton.onClick.AddListener(EnableNextLevel);
            }
            if (_repeatLevelButton != null)
            {
                _repeatLevelButton.onClick.AddListener(RepeatLevel);
            }
        }

        private void OnDisable()
        {
            if (_nextLevelButton != null)
            {
                _nextLevelButton.onClick.RemoveListener(EnableNextLevel);
            }
            if (_repeatLevelButton != null)
            {
                _repeatLevelButton.onClick.RemoveListener(RepeatLevel);
            }
        }

        public void EnableCurrentScene()
        {
            string sceneToLoad = YandexGame.savesData.indexCurrentScene == 0
                ? SceneNames.Level1
                : SceneManager.GetSceneByBuildIndex(YandexGame.savesData.indexCurrentScene).name;

            SceneManager.LoadScene(sceneToLoad);
        }

        private void EnableNextLevel()
        {
            _saver.Save();
            SceneManager.LoadScene(_nextSceneName);
            YandexGame.FullscreenShow();
        }

        private void RepeatLevel()
        {
            _saver.Save();
            SceneManager.LoadScene(_currentSceneName);
            YandexGame.FullscreenShow();
        }
    }
}