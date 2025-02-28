using Character;
using UnityEngine;

namespace UI.Reward
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                _winScreen.SetActive(true);
            }
        }
    }
}