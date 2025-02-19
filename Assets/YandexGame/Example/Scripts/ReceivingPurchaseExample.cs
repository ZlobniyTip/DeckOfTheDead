using Character;
using UI.Shop;
using UnityEngine;
using UnityEngine.Events;

namespace YG.Example
{
    [HelpURL("https://www.notion.so/PluginYG-d457b23eee604b7aa6076116aab647ed#10e7dfffefdc42ec93b39be0c78e77cb")]
    public class ReceivingPurchaseExample : MonoBehaviour
    {
        [SerializeField] UnityEvent successPurchased;
        [SerializeField] UnityEvent failedPurchased;
        [SerializeField] private Buyer _buyer;
        [SerializeField] private LootBox _lootBox;
        [SerializeField] private SaverTest _saver;

        private void OnEnable()
        {
            YandexGame.PurchaseSuccessEvent += SuccessPurchased;
            YandexGame.PurchaseFailedEvent += FailedPurchased;
        }

        private void OnDisable()
        {
            YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
            YandexGame.PurchaseFailedEvent -= FailedPurchased;
        }

        void SuccessPurchased(string id)
        {
            successPurchased?.Invoke();

            switch (id)
            {
                case "Box":
                    _lootBox.OpenLootBox();
                    break;

                case "Money":
                    _buyer.GetMoney(20000);
                    break;

                case "Money2":
                    _buyer.GetMoney(50000);
                    break;
            }

            _saver.Save();
        }

        void FailedPurchased(string id)
        {
            failedPurchased?.Invoke();
        }
    }
}