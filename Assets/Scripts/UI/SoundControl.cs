using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class SoundControl : MonoBehaviour
    {
        [SerializeField] private Slider _sound;

        private void OnEnable()
        {
            _sound.value = AudioListener.volume;
        }

        private void OnDisable()
        {
            YandexGame.SaveProgress();
        }

        public void ChangeSound()
        {
            AudioListener.volume = _sound.value;
            YandexGame.savesData.sound = AudioListener.volume;
        }
    }
}