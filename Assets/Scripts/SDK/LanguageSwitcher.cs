using UnityEngine;
using UnityEngine.UI;
using YG;

namespace SDK
{
    public class LanguageSwitcher : MonoBehaviour
    {
        private readonly string Rus = "ru";
        private readonly string Tur = "tr";
        private readonly string Eng = "en";

        [SerializeField] private Button _rs;
        [SerializeField] private Button _tr;
        [SerializeField] private Button _en;

        private void OnEnable()
        {
            _rs.onClick.AddListener(OnSetRusLanguage);
            _tr.onClick.AddListener(OnSetTrLanguage);
            _en.onClick.AddListener(OnSetEngLanguage);
        }

        private void OnDisable()
        {
            _rs.onClick.RemoveListener(OnSetRusLanguage);
            _tr.onClick.RemoveListener(OnSetTrLanguage);
            _en.onClick.RemoveListener(OnSetEngLanguage);
        }

        private void OnSetRusLanguage()
        {
            YandexGame.SwitchLanguage(Rus);
        }

        private void OnSetTrLanguage()
        {
            YandexGame.SwitchLanguage(Tur);
        }

        private void OnSetEngLanguage()
        {
            YandexGame.SwitchLanguage(Eng);
        }
    }
}