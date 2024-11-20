using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Product> _products;
    [SerializeField] private ItemView _template;
    [SerializeField] private GameObject _itemContainer;

    private readonly List<ItemView> _content = new();

    private void OnEnable()
    {
        foreach (var item in _products)
            AddItemView(item);
    }

    private void OnDisable()
    {
        foreach (var item in _content)
        {
            item.PurchaseButtonPressed -= OnPurchaseButtonPressed;
            item.EquipButtonPressed -= OnEquipButtonPressed;
            Destroy(item.gameObject);
        }

        _content.Clear();
    }

    public void GetListProducts(List<Product> products)
    {
        foreach (var item in products)
        {
            AddItemView(item);
        }
    }

    private void AddItemView(Product product)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.Init(product);
        view.PurchaseButtonPressed += OnPurchaseButtonPressed;
        view.EquipButtonPressed += OnEquipButtonPressed;
        _content.Add(view);
    }

    private void OnPurchaseButtonPressed(ItemView view)
    {
    }

    private void OnEquipButtonPressed(ItemView view)
    {
    }
}
