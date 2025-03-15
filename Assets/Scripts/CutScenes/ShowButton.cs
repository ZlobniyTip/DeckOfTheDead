using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CutScenes
{
    public class ShowButton : MonoBehaviour
    {
        private readonly int GameStartScene = 2;

        [SerializeField] private CutScenes _cutScenes;
        [SerializeField] private Button _buttonNextLevel;

        private void Start()
        {
            _cutScenes.EndCutScene += OnShowFirstLevelButton;
            _buttonNextLevel.onClick.AddListener(OnStartFirstLevel);
        }

        private void OnDisable()
        {
            _cutScenes.EndCutScene -= OnShowFirstLevelButton;
            _buttonNextLevel.onClick.RemoveListener(OnStartFirstLevel);
        }

        private void OnShowFirstLevelButton()
        {
            _buttonNextLevel.gameObject.SetActive(true);
        }

        private void OnStartFirstLevel()
        {
            SceneManager.LoadScene(GameStartScene);
        }
    }
}