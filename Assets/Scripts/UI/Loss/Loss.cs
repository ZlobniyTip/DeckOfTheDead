using UnityEngine;

public class Loss : MonoBehaviour
{
    [SerializeField] private GameObject _lossScreen;
    [SerializeField] private Character _character;

    private void Start()
    {
        _character.Died += OpenLossScreen;
    }

    private void OnDisable()
    {
        _character.Died -= OpenLossScreen;
    }

    private void OpenLossScreen()
    {
        _lossScreen.gameObject.SetActive(true);
    }
}