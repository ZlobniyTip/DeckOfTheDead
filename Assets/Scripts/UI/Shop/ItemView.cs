using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private GameObject _equippedLabel;

    [SerializeField] protected Button _equipButton;

    [SerializeField] protected TMP_Text _name;
    [SerializeField] protected Image _icon;

    private IProduct _product;
    private bool _isShopItem = false;
    private Shop _shop;

    public event UnityAction<ItemView> PurchaseButtonPressed;
    public event UnityAction<ItemView> EquipButtonPressed;

    public IProduct Product => _product;

    private void OnDisable()
    {
        if (_isShopItem)
        {
            _equipButton.onClick.RemoveListener(OnEquipButtonPressed);
            _purchaseButton.onClick.RemoveListener(OnPurchaseButtonPressed);
        }
    }

    private void OnDestroy()
    {
        if (_isShopItem)
        {
            _shop.PlayerEquippedItem -= ShowEquipButton;
            _product.State.Changed -= OnWeaponStateChanged;
        }
    }

    public void Init(IProduct product, Shop shop)
    {
        _shop = shop;
        _isShopItem = true;
        _product = product;

        if (_isShopItem)
        {
            _product.State.Changed += OnWeaponStateChanged;
            _equipButton.onClick.AddListener(OnEquipButtonPressed);
            _purchaseButton.onClick.AddListener(OnPurchaseButtonPressed);
        }

        UpdateView();
    }

    private void UpdateView()
    {
        _name.text = LeanLocalization.GetTranslationText(_product.Name);

        if (_name.text == null)
            _name.text = _product.Name;

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

                if (_isShopItem)
                    _shop.PlayerEquippedItem += ChangeStatus;
                break;
        }
    }

    private void ChangeStatus()
    {
        _product.State.SetStatus(ItemStatus.Purchased);
        ShowEquipButton();
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