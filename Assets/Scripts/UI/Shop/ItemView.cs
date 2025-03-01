using Card;
using Lean.Localization;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private GameObject _equippedLabel;

        [SerializeField] private Button _equipButton; 
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _icon;

        private IProduct _product;
        private bool _isShopItem = false;
        private Store _shop;

        protected Button EquipButton => _equipButton; 

        protected TMP_Text Name => _nameText;

        protected Image Icon => _icon;

        public IProduct Product => _product;

        public event UnityAction<ItemView> PurchaseButtonPressed;

        public event UnityAction<ItemView> EquipButtonPressed;

        private void OnDisable()
        {
            if (_isShopItem)
            {
                EquipButton.onClick.RemoveListener(OnEquipButtonPressed);
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

        public void Init(IProduct product, Store shop)
        {
            _shop = shop;
            _isShopItem = true;
            _product = product;

            if (_isShopItem)
            {
                _product.State.Changed += OnWeaponStateChanged;
                EquipButton.onClick.AddListener(OnEquipButtonPressed);
                _purchaseButton.onClick.AddListener(OnPurchaseButtonPressed);
            }

            UpdateView();
        }

        private void UpdateView()
        {
            Name.text = LeanLocalization.GetTranslationText(_product.Name);

            if (Name.text == null)
                Name.text = _product.Name;

            _price.text = _product.Price.ToString();
            Icon.sprite = _product.Icon;

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
            EquipButton.gameObject.SetActive(isEquip);
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
}