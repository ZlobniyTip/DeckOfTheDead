using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private GameObject _equippedLabel;

    private Product _product;

    public event UnityAction<ItemView> PurchaseButtonPressed;
    public event UnityAction<ItemView> EquipButtonPressed;

    public Product Product => _product;

    private void OnEnable()
    {
        _equipButton.onClick.AddListener(OnEquipButtonPressed);
        _purchaseButton.onClick.AddListener(OnPurchaseButtonPressed);
    }

    private void OnDisable()
    {
        _equipButton.onClick.RemoveListener(OnEquipButtonPressed);
        _purchaseButton.onClick.RemoveListener(OnPurchaseButtonPressed);
    }

    private void OnDestroy()
    {
        _product.State.Changed -= OnWeaponStateChanged;
    }

    public void Init(Product product)
    {
        _product = product;

        _product.State.Changed += OnWeaponStateChanged;

        UpdateView();
    }

    private void UpdateView()
    {
        _label.text = _product.Name;
        _price.text = _product.Price.ToString();
        _icon.sprite = _product.Icon;

        switch (_product.State.Status)
        {
            case ItemStatus.NotPurchased:
                ShowPurchaseButton();
                break;
            case ItemStatus.Purchased:
                ShowEquipButton();
                break;
            case ItemStatus.Equipped:
                ShowEquippedLabel();
                break;
        }
    }

    private void ShowButton(bool isPurchase, bool isEquip, bool isEquipped)
    {
        _purchaseButton.gameObject.SetActive(isPurchase);
        _equipButton.gameObject.SetActive(isEquip);
        _equippedLabel.SetActive(isEquipped);
    }

    private void ShowPurchaseButton()
    {
        ShowButton(true, false, false);
    }

    private void ShowEquipButton()
    {
        ShowButton(false, true, false);
    }

    private void ShowEquippedLabel()
    {
        ShowButton(false, false, true);
    }

    private void OnWeaponStateChanged()
    {
        UpdateView();
    }

    private void OnPurchaseButtonPressed()
    {
        PurchaseButtonPressed?.Invoke(this);
    }

    private void OnEquipButtonPressed()
    {
        EquipButtonPressed?.Invoke(this);
    }
}