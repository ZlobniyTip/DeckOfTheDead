using UnityEngine;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public void OpenMenu(GameObject panel)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }

        public void CloseMenu(GameObject panel)
        {
            Time.timeScale = 1;
            panel.SetActive(false);
        }
    }
}