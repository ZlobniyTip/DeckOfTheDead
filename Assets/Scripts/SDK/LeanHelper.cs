using Lean.Localization;
using UnityEngine;
using YG;

namespace UI
{
    public class LeanHelper : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;

        private void Start()
        {
            ChangeLeanLanguage(YandexGame.lang);
            YandexGame.SwitchLangEvent += ChangeLeanLanguage;
        }

        private void OnDisable()
        {
            YandexGame.SwitchLangEvent -= ChangeLeanLanguage;
        }

        private void ChangeLeanLanguage(string lang)
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
        }
    }
}