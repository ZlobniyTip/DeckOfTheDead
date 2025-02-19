using UnityEngine;
using UnityEngine.UI;
using YG;

namespace SDK
{
    public class LanguageSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _rs;
        [SerializeField] private Button _tr;
        [SerializeField] private Button _en;

        private readonly string _rus = "ru";
        private readonly string _tur = "tr";
        private readonly string _eng = "en";

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
            YandexGame.SwitchLanguage(_rus);
        }

        private void SetTrLanguage()
        {
            YandexGame.SwitchLanguage(_tur);
        }

        private void SetEngLanguage()
        {
            YandexGame.SwitchLanguage(_eng);
        }
    }
}