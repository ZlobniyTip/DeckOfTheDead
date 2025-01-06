using UnityEngine;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public bool IsPaused { get; private set; } = false;

        public void OpenMenu(GameObject panel)
        {
            IsPaused = true;
            panel.SetActive(true);
        }

        public void CloseMenu(GameObject panel)
        {
            IsPaused = false;
            panel.SetActive(false);
        }
    }
}