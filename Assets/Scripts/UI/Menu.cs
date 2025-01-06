using UnityEngine;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public bool IsPaused { get; private set; } = false;

        public void OpenMenu(GameObject panel)
        {
            IsPaused = true;
            Time.timeScale = 0;
            panel.SetActive(true);
        }

        public void CloseMenu(GameObject panel)
        {
            IsPaused = false;
            Time.timeScale = 1;
            panel.SetActive(false);
        }
    }
}