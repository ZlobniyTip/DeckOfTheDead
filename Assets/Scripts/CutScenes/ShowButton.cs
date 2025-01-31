using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowButton : MonoBehaviour
{
    [SerializeField] private CutScenes _cutScenes;
    [SerializeField] private Button _buttonNextLevel;

    private void Start()
    {
        _cutScenes.EndCutScene += ShowFirstLevelButton;
        _buttonNextLevel.onClick.AddListener(StartFirstLevel);
    }

    private void OnDisable()
    {
        _cutScenes.EndCutScene -= ShowFirstLevelButton;
        _buttonNextLevel.onClick.RemoveListener(StartFirstLevel);
    }

    private void ShowFirstLevelButton()
    {
        _buttonNextLevel.gameObject.SetActive(true);
    }

    private void StartFirstLevel()
    {
        SceneManager.LoadScene(2);
    }
}