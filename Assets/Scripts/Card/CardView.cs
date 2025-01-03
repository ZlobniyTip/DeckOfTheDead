using UnityEngine;
using UnityEngine.UI;

public abstract class CardView : ItemView
{
    [SerializeField] private Image _activity;

    protected CardData _cardData;
    private DragAndDropCardUnit _dragAndDrop;
    private DragAndDropCardWeapon _dragAndDropWeapon;

    public CardData Card => _cardData;

    private void Awake()
    {
        _dragAndDrop = GetComponent<DragAndDropCardUnit>();
        _dragAndDropWeapon = GetComponent<DragAndDropCardWeapon>();
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
        if (_dragAndDrop != null)
            _dragAndDrop.enabled = isActiv;

        if (_dragAndDropWeapon != null)
            _dragAndDropWeapon.enabled = isActiv;
    }
}
