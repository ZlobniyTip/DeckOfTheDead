using Character;
using UnityEngine;

namespace UI.Reward
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player character))
            {
                _winScreen.SetActive(true);
            }
        }
    }
}