using UnityEngine;

namespace UI
{
    public class PanelSwither : MonoBehaviour
    {
        public void OpenPanel(GameObject panel)
        {
            panel.SetActive(true);
        }

        public void ClosePanel(GameObject panel)
        {
            panel.SetActive(false);
        }
    }
}