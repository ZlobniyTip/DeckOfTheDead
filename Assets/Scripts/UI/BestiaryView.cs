using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameView;
    [SerializeField] private TMP_Text _skillNameView;
    [SerializeField] private TMP_Text _skillDescriptionView;
    [SerializeField] private Image _image;

    public void Initialize(ZombieData zombieData)
    {
        _nameView.text = LeanLocalization.GetTranslationText(zombieData.Name);
        _skillNameView.text = LeanLocalization.GetTranslationText(zombieData.SkillName);
        _skillDescriptionView.text = LeanLocalization.GetTranslationText(zombieData.SkillDescription);
        _image.sprite = zombieData.Image;
    }
}