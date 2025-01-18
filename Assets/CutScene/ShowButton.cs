using UnityEngine;

public class ShowButton : MonoBehaviour
{
    [SerializeField] private CutScenes _cutScenes;
    [SerializeField] private GameObject _buttonNextLevel;

    private void Start()
    {
        _cutScenes.EndCutScene += ShowFirstLevelButton;
    }

    private void OnDisable()
    {
        _cutScenes.EndCutScene -= ShowFirstLevelButton;
    }

    private void ShowFirstLevelButton()
    {
        _buttonNextLevel.SetActive(true);
    }
}