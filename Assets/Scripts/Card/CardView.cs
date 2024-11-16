using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Card _card;

    [SerializeField] private Image _image;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _energy;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _speed;
    [SerializeField] private TMP_Text _ability;

    private void Start()
    {
        _image.sprite = _card.Icon;
        _name.text = _card.Name;
        _energy.text = _card.Energy.ToString();
        _level.text = _card.Level.ToString();
        _health.text = _card.Health.ToString();
        _damage.text = _card.Damage.ToString();
        _speed.text = _card.Speed.ToString();
        _ability.text = _card.Ability;
    }
}