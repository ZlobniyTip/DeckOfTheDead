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
}