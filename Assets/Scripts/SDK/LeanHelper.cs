using System;
using Lean.Localization;
using UnityEngine;
using YG;

namespace SDK
{
    public class LeanHelper : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;

        public event Action LanguageChanged;

        private void Start()
        {
            OnLealLanguageChanged(YandexGame.lang);
            YandexGame.SwitchLangEvent += OnLealLanguageChanged;
        }

        private void OnDisable()
        {
            YandexGame.SwitchLangEvent -= OnLealLanguageChanged;
        }

        private void OnLealLanguageChanged(string lang)
        {
            switch (lang)
            {
                case "ru":
                    _leanLocalization.SetCurrentLanguage("Russian");
                    break;
                case "tr":
                    _leanLocalization.SetCurrentLanguage("Turkish");
                    break;
                case "en":
                    _leanLocalization.SetCurrentLanguage("English");
                    break;
            }

            LanguageChanged?.Invoke();
        }
    }
}