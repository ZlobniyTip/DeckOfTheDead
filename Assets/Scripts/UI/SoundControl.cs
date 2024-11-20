using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundControl : MonoBehaviour
    {
        [SerializeField] private Slider _sound;

        private void OnEnable()
        {
            _sound.value = AudioListener.volume;
        }

        public void ChangeSound()
        {
            AudioListener.volume = _sound.value;
        }
    }
}