using Character;
using UnityEngine;

namespace UI.Loss
{
    public class Loss : MonoBehaviour
    {
        [SerializeField] private GameObject _lossScreen;
        [SerializeField] private Player _character;

        private void Start()
        {
            _character.Died += OnOpenLossScreen;
        }

        private void OnDisable()
        {
            _character.Died -= OnOpenLossScreen;
        }

        private void OnOpenLossScreen()
        {
            _lossScreen.gameObject.SetActive(true);
        }
    }
}