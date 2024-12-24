using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardView : ItemView
{
    [SerializeField] private Image _activity;

    protected CardData _cardData;
    private DragAndDrop _dragAndDrop;

    public CardData Card => _cardData;

    private void Awake()
    {
        _dragAndDrop = GetComponent<DragAndDrop>();
    }

    public abstract void Initialize(CardData cardData);


    public void ActivateCard()
    {
        _activity.gameObject.SetActive(false);
        SwitchDragAndDrop(true);
    }

    public void DeactivateCard()
    {
        _activity.gameObject.SetActive(true);
        SwitchDragAndDrop(false);
    }

    public void SwitchDragAndDrop(bool isActiv)
    {
        _dragAndDrop.enabled = isActiv;
    }
}
