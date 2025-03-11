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
            _rs.onClick.AddListener(SetRusLanguage);
            _tr.onClick.AddListener(SetTrLanguage);
            _en.onClick.AddListener(SetEngLanguage);
        }

        private void OnDisable()
        {
            _rs.onClick.RemoveListener(SetRusLanguage);
            _tr.onClick.RemoveListener(SetTrLanguage);
            _en.onClick.RemoveListener(SetEngLanguage);
        }

        private void SetRusLanguage()
        {
            YandexGame.SwitchLanguage(Rus);
        }

        private void SetTrLanguage()
        {
            YandexGame.SwitchLanguage(Tur);
        }

        private void SetEngLanguage()
        {
            YandexGame.SwitchLanguage(Eng);
        }
    }
}