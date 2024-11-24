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
    [SerializeField] private TMP_Text _ability;

    [SerializeField] private Image _activity;

    private DragAndDrop _dragAndDrop;

    public Card Card => _card;

    private void Awake()
    {
       _dragAndDrop = GetComponent<DragAndDrop>();
    }

    private void Start()
    {
        _image.sprite = _card.Icon;
        _name.text = _card.Name;
        _energy.text = _card.Energy.ToString();
        _level.text = _card.Level.ToString();
        _health.text = _card.Health.ToString();
        _damage.text = _card.Damage.ToString();
        _ability.text = _card.Ability;
    }

    public void Initialized(Card card)
    {
        _card = card;   
    }

    public void ActivateCard()
    {
        _activity.gameObject.SetActive(false);
        _dragAndDrop.enabled = true;
    }

    public void DeactivateCard()
    {
        _activity.gameObject.SetActive(true);
        _dragAndDrop.enabled = false;
    }
}